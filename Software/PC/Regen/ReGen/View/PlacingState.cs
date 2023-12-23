using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ReGen.View
{
    /// <summary>
    /// Classe astratta che rappresenta lo stato del piazzamento
    /// </summary>
    public abstract class PlacingState
    {
        public abstract Color colorOf(FigurePayload fp);
        public abstract Color colorOf(FigurePayloadGroup fp);
        public abstract Color colorOf(FigureRanieriPayloadGroup fp);
        public abstract Color colorOf(FigurePayloadWithRightSurplus fp);
    }
    /// <summary>
    /// Classe che rappresenta lo stato "placed"
    /// </summary>
    public class PlacingState_placed : PlacingState
    {
        /// <summary>
        /// Colora la figura che rappresenta il PayloadPlaced 
        /// </summary>
        /// <param name="fp">FigurePayload da colorare</param>
        /// <returns>Il colore in base allo stato del Payload</returns>
        public override Color colorOf(FigurePayload fp)
        {
            //TODO fixed
            //if (new PayloadGroup(fp.pp).isCorrectlyPlaced())
            //if (!fp.pp.collides(fp.pp.group))
            if (fp.pp.group.isCorrectlyPlaced())
            {
                if (!fp.selected)
                {
                    return Program.colorPlacedPayload(fp.pp.getPayloadStrategy().getName());
                }
                else
                {
                    return Program.colorSelectedPayload;
                }
            }
            else
            {
                return Program.colorIncorrectlyPlacedPayload;
            }
        }
        /// <summary>
        /// Colora la figura che rappresenta il PayloadGroup "placed"
        /// </summary>
        /// <param name="fp">FigurePayloadGroup da colorare</param>
        /// <returns>Il colore per la figura PayloadGroup "placed"</returns>
        public override Color colorOf(FigurePayloadGroup fp)
        {
            return Color.Black;
        }
        /// <summary>
        /// Colora la figura che rappresenta il PayloadGroup Ranieri
        /// </summary>
        /// <param name="fp">FigurePayloadGroup Ranieri da colorare</param>
        /// <returns>Il colore per la figura PayloadGroup Ranieri "placed"</returns>
        public override Color colorOf(FigureRanieriPayloadGroup fp)
        {
            return Program.colorOfFinger;
            
        }
        /// <summary>
        /// Colora la figura che rappresenta il PayloadGroupWithRightSurplus
        /// </summary>
        /// <param name="fp">FigurePayloadGroupWithRightSurplus da colorare</param>
        /// <returns>Il colore per la figura PayloadGroupWithRightSurplus "placed"</returns>
        public override Color colorOf(FigurePayloadWithRightSurplus fp)
        {
            return Program.colorOfSurplus;
        }
    }
    /// <summary>
    /// Classe che rappresenta lo stato "onDepot"
    /// </summary>
    public class PlacingState_onDepot : PlacingState
    {
        /// <summary>
        /// Colora la figura che rappresenta il Payload "onDepot"
        /// </summary>
        /// <param name="fp">FigurePayload da colorare</param>
        /// <returns>Il colore per la figura Payload "onDepot" </returns>
        public override Color colorOf(FigurePayload fp)
        {
            return Program.colorDraggingPayloadOnDepot;
        }
        /// <summary>
        /// Colora la figura che rappresenta il PayloadGroup "onDepot"
        /// </summary>
        /// <param name="fp">FigurePayloadGroup da colorare</param>
        /// <returns>Il colore per la figura PayloadGroup "onDepot"</returns>
        public override Color colorOf(FigurePayloadGroup fp)
        {
            return Color.Black;
        }
        /// <summary>
        /// Colora la figura che rappresenta il PayloadGroup Ranieri "onDepot"
        /// </summary>
        /// <param name="fp">FigureRanieriPayloadGroup da colorare</param>
        /// <returns>Il colore per la figura RanieriPayloadGroup "onDepot"</returns>
        public override Color colorOf(FigureRanieriPayloadGroup fp)
        {
            return Program.colorOfFinger;
        }
        /// <summary>
        /// Colora la figura che rappresenta il PayloadWithRightSurplus "onDepot"
        /// </summary>
        /// <param name="fp">FigurePayloadGroupWithRightSurplus da coloraree</param>
        /// <returns>Il colore per la figura PayloadWithRightSurplus "onDepot"</returns>
        public override Color colorOf(FigurePayloadWithRightSurplus fp)
        {
            return Program.colorOfSurplus;
        }
    }
    /// <summary>
    /// Classe che rappresenta lo stato "onPlatform"
    /// </summary>
    public class PlacingState_onPlatform : PlacingState
    {
        /// <summary>
        /// Colora la figura che rappresenta il Payload "onPlatform"
        /// </summary>
        /// <param name="fp">FigurePayload da colorare</param>
        /// <returns>Il colore per la figura Payload "onPlatform"</returns>
        public override Color colorOf(FigurePayload fp)
        {
            return Program.colorDraggingPayloadOnPlatform;
        }
        /// <summary>
        /// Colora la figura che rappresenta il PayloadGroup
        /// </summary>
        /// <param name="fp">FigurePayloadGroup da colorare</param>
        /// <returns>Il colore per la figura PayloadGroup "onPlatform"</returns>
        public override Color colorOf(FigurePayloadGroup fp)
        {
            return Color.Black;
        }
        /// <summary>
        /// Colora la figura che rappresenta il RanieriPayloadGroup "onPlatform"
        /// </summary>
        /// <param name="fp">FigureRanieriPayloadGroup da colorare</param>
        /// <returns>Il colore per la figura RanieriPayloadGroup "onPlatform"</returns>
        public override Color colorOf(FigureRanieriPayloadGroup fp)
        {
            return Program.colorOfFinger;
        }
        /// <summary>
        /// Colora la figura che rappresenta il PayloadWithRightSurplus "onPlatform"
        /// </summary>
        /// <param name="fp">FigurePayloadWithRightSurplus da colorare</param>
        /// <returns>Il colore per la figura PayloadWithRightSurplus "onPlatform"</returns>
        public override Color colorOf(FigurePayloadWithRightSurplus fp)
        {
            return Program.colorOfSurplus;
        }
    }
}
