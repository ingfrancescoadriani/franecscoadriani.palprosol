using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReGen
{
    /// <summary>
    /// Classe che rappresenta la collisione tra PayloadGroup
    /// </summary>
    public class Collision : ProblemOfPositioning
    {
        PayloadGroup pCollidingWith;
        /// <summary>
        /// Costruttore per la classe Collision
        /// </summary>
        /// <param name="pCollidingWith">PayloadGroup con cui si ha collisione</param>
        public Collision(PayloadGroup pCollidingWith)
            : base()
        {
            this.pCollidingWith=pCollidingWith;
        }
    }
}
