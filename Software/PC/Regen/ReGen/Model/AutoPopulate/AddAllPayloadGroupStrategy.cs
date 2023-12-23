using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;

namespace ReGen.Model.AutoPopulate
{
    /// <summary>
    /// Classe Strategy che controlla se il PayloadGroup si può inserire o meno
    /// </summary>
    public class AddAllPayloadGroupStrategy : FindGoodPayloadGroupToAddStrategy
    {
        /// <summary>
        /// Controlla se il PayloadGroup si può inserire nel Layer
        /// </summary>
        /// <param name="layer">Layer in cui si vuole inserire il PayloadGroup</param>
        /// <param name="possiblePayloadGroup">PayloadGroup che si vuole inserire</param>
        /// <param name="possiblePointOfApplication">Punto in cui si vuole inserire il PayloadGroup (possibile centro del PayloadGroup nel Layer)</param>
        /// <returns>Null se il PayloadGroup non può essere inserito. Se il PayloadGroup può essere inserito ritorna il possiblePointOfApplication</returns>
        public override Point2FWithDirection canAddPayloadGroupToLayer(Layer layer, PayloadGroup possiblePayloadGroup, Point2FWithDirection possiblePointOfApplication)
        {
            Point2FWithDirection res = null;
            if (layer.canCointainsPayloadGroup(possiblePayloadGroup.withCenter(possiblePointOfApplication)))
                res = possiblePointOfApplication;
            return res;
        }
    }
}
