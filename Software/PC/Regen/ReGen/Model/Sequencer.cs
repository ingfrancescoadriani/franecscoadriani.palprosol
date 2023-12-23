using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;

namespace ReGen.Model
{
    //TODO da capire
    public static class Sequencer
    {
        // sezione del rank statico impostato (per autogenerazione)
        // il rank si riferisce all'ordine di creazione
        public static Dictionary<long, int> fixedRankList = new Dictionary<long, int>();


        /// <summary>
        /// Resetta la lista dei rank fissi
        /// </summary>
        public static void clearFixedRankList()
        {
            fixedRankList = new Dictionary<long, int>();
        }
        /// <summary>
        /// Aggiunge alla lista dei rank fissi l'id di un payload e il rank
        /// </summary>
        /// <param name="payloadGroupId">Nuovo PayloadGroupId</param>
        /// <param name="rank">Rank</param>
        public static void addPayloadGroupAtFixedRankList(long payloadGroupId, int rank)
        {
            fixedRankList.Add(payloadGroupId, rank);
        }
        /// <summary>
        /// Ritorna il rank dall'id passato
        /// </summary>
        /// <param name="payloadGroupId">Id di cui si cerca il rank</param>
        /// <returns></returns>
        public static int getRankOfPayloadGroup(long payloadGroupId)
        {
            return fixedRankList[payloadGroupId];
        }

        public static void fixPositionAt(Layer l, PayloadGroup pg, int pos)
        {
            foreach (PayloadGroup pg_ in l.listPayloadGroupPlaced)
            {
                if (pg_.fixedProgressivePosition == pos)
                    pg_.fixedProgressivePosition = 0;
            }
            pg.fixedProgressivePosition = pos;
        }

        private static SortedDictionary<int, PayloadGroup> positionListWithFixed(Layer l)
        {
            SortedDictionary<int, PayloadGroup> dictWithOutFixed = positionListWithOutFixed(l);
            SortedDictionary<int, PayloadGroup> res = new SortedDictionary<int, PayloadGroup>();
            int pos = 0;
            int indexOnRank = 0;
            while (pos < l.listPayloadGroupPlaced.Count && indexOnRank < l.listPayloadGroupPlaced.Count)
            {
                PayloadGroup pgWithPosFixed = null;
                foreach (PayloadGroup pg in l.listPayloadGroupPlaced)
                {
                    if (pg.fixedProgressivePosition>0 && (pg.fixedProgressivePosition-1) == pos)
                    {
                        pgWithPosFixed = pg;
                        break;
                    }
                }
                // se ho trovato un gruppo sulla lista dei fissati lo aggiungo alla posizione pos
                if (pgWithPosFixed != null)
                    res.Add(pos++, pgWithPosFixed);
                else
                {   
                    // uso la lista dei rank ma facendo attenzione che
                    // se il payload non è contenuto nel dizionario dato che prima o poi vorrà sostituire la posizione a qualcun altro altrimenti se non ci fosse questa if si aggiungerebbe due volte uno stesso gruppo
                    PayloadGroup pgOfId = null;
                    foreach (PayloadGroup pg in l.listPayloadGroupPlaced)
                    {
                        if (pg.fixedProgressivePosition>0 && pg.getId() == dictWithOutFixed[indexOnRank].getId())
                        {
                            pgOfId = pg;
                            break;
                        }
                    }
                    if (pgOfId==null)
                        res.Add(pos++, dictWithOutFixed[indexOnRank]);
                    indexOnRank++;
                }
            }
            return res;
        }

        private static SortedDictionary<int, PayloadGroup> positionListWithOutFixed(Layer l)
        {
            SortedDictionary<int, PayloadGroup> res = new SortedDictionary<int, PayloadGroup>();
            int k = 0;
            foreach (PayloadGroup pg in getRankListWithOutFixed(l))
                res.Add(k++, pg);
            return res;
        }

        private static List<PayloadGroup> getRankListWithOutFixed(Layer l)
        {
            List<PayloadGroup> res = new List<PayloadGroup>();
            SortedDictionary<double, List<PayloadGroup>> rankList = getRankListOfListAtSameRank(l);
            foreach (double rank in rankList.Keys)
            {
                List<PayloadGroup> rankListAtSameRank = rankList[rank];
                for (int i = 0; i < rankListAtSameRank.Count; i++)
                    res.Add(rankListAtSameRank[i]);
            }
            return res;
        }

        private static SortedDictionary<double, List<PayloadGroup>> getRankListOfListAtSameRank(Layer l)
        {
            SortedDictionary<double, List<PayloadGroup>> rankListOfListAtSameRank = new SortedDictionary<double, List<PayloadGroup>>();
            foreach (PayloadGroup pg in l.listPayloadGroupPlaced)
            {
                if (!MainForm.instance.listPayloadGroupSelected.Contains(pg))
                {
                    double rank = pg.getRank(MainForm.instance.work.palletOnSystem);
                    if (!rankListOfListAtSameRank.ContainsKey(rank))
                        rankListOfListAtSameRank.Add(rank, new List<PayloadGroup>());
                    rankListOfListAtSameRank[rank].Add(pg);
                }
            }
            foreach (PayloadGroup pg in MainForm.instance.listPayloadGroupSelected)
            {
                double rank = pg.getRank(MainForm.instance.work.palletOnSystem);
                if (!rankListOfListAtSameRank.ContainsKey(rank))
                {
                    rankListOfListAtSameRank.Add(rank, new List<PayloadGroup>());
                    rankListOfListAtSameRank[rank].Add(pg);
                }
                else if (!rankListOfListAtSameRank[rank].Contains(pg))
                {
                    rankListOfListAtSameRank[rank].Add(pg);
                }
            }

            return rankListOfListAtSameRank;
        }

        public static long getProgressive(long payloadId, Layer l)
        {
            long ret = 0;
            PayloadGroup pgOfId = null;
            foreach (PayloadGroup pg in l.listPayloadGroupPlaced)
            {
                if (pg.getId() == payloadId)
                {
                    pgOfId = pg;
                    break;
                }
            }
            if (pgOfId != null)
            {
                if (pgOfId.fixedProgressivePosition > 0)
                    ret = pgOfId.fixedProgressivePosition;
                else
                {
                    SortedDictionary<int, PayloadGroup> posList = positionListWithFixed(l);
                    foreach (int pos in posList.Keys)
                    {
                        if (posList[pos].getId() == payloadId)
                        {
                            ret = pos + 1;
                            break;
                        }
                    }

                    // OLD CODE WITHOUT FIXED RANK 
                    //int count = 0;
                    //foreach (double rank in rankList.Keys)
                    //    count += rankList[rank].Count;

                    //int res = 0; int k = -1;
                    //foreach (double rank in rankList.Keys)
                    //{
                    //    List<PayloadGroup> rankListAtSameRank = rankList[rank];
                    //    for (int i = rankListAtSameRank.Count - 1; i >= 0; i--)
                    //    {
                    //        k++;
                    //        if (rankListAtSameRank[i].getId() == payloadId)
                    //        {
                    //            res = count - k;
                    //            break;
                    //        }
                    //    }
                    //}
                }
            }
            return ret;
        }

        private static List<PayloadGroup> sortedPayloadWithSameRank(List<PayloadGroup> unsortedPayloadWithSameRank)
        {
            List<PayloadGroup> res = new List<PayloadGroup>();
            foreach (PayloadGroup pg in unsortedPayloadWithSameRank)
            {
                res.Insert(0,pg);
            }
            return res;
        }
    }
}
