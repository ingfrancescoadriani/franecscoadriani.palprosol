using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sintec.Tool;

namespace ReGen.Model.AutoPopulate
{
    /// <summary>
    /// Classe Strategy astratta che controlla se è possibile inserire un PayloadGroup in un Layer
    /// </summary>
    public abstract class FindGoodPayloadGroupToAddStrategy
    {
        /// <summary>
        /// Definisce se un payload può essere aggiunto ad un layer con altri PayloadGroup
        /// </summary>
        /// <param name="layer">Layer sul quale si vogliono inserire i PayloadGroup</param>
        /// <param name="possiblePayloadGroup">PayloadGroup che si vuole inserire</param>
        /// <param name="possiblePointOfApplication">Punti di applicazione dei Payload</param>
        /// <returns>Punto di inserimento del PayloadGroup</returns>
        public abstract Point2FWithDirection canAddPayloadGroupToLayer(Layer layer, PayloadGroup possiblePayloadGroup, Point2FWithDirection possiblePointOfApplication);
        // definisce se un payload può essere aggiunto ad un layer con altri payloadgroup inseriti in base a tutti i filtri possibili
        // il layer fornisce tutte le informazioni riguardanti le sagome dei payload già aggiungti e del pallet
        // qui va inserito anche il controllo di interferenza tra i pezzi già inseriti
        // il payload e i punti forniti sono già stati filtrati in base ad altre strategie
    }
}
