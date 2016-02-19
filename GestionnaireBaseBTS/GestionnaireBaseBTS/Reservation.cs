using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionnaireBaseBTS
{
    public class Reservation : ObservableObject
    {
        #region Fields
        private String _NomAgent;
        private String _FinReservation;
        private String _Commentaire;
        #endregion

        #region Properties
        public string NomAgent
        {
            get { return _NomAgent; }
            set { _NomAgent = value; OnPropertyChanged("NomAgent"); }
        }
        public string FinReservation
        {
            get { return _FinReservation; }
            set { _FinReservation = value; OnPropertyChanged("FinReservation"); }
        }
        public string Commentaire
        {
            get { return _Commentaire; }
            set { _Commentaire = value; OnPropertyChanged("Commentaire"); }
        }
        #endregion

        #region Constructors
        public Reservation(String nomAgent, String finReservation, String commentaire)
        {
            _NomAgent = nomAgent;
            _FinReservation = finReservation;
            _Commentaire = commentaire;
        }
        #endregion
    }
}
