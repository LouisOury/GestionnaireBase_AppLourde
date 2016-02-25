using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionnaireBaseBTS.OV
{
    public class OVCommandeRoutee : ICommand
    {
        #region private fields
        private readonly Action execute;
        private readonly Action<object> executeWithParameter;
        private readonly Func<bool> canExecute;
        private readonly Func<object, bool> canExecuteWithParameter;
        #endregion

        /// <summary>
        ///  
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if ((canExecute != null) || (canExecuteWithParameter != null)) CommandManager.RequerySuggested += value;
            }
            remove
            {
                if ((canExecute != null) || (canExecuteWithParameter != null)) CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe CommandHlp
        /// </summary>
        /// <param name="execute">action à exécuter.</param>
        public OVCommandeRoutee(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe CommandHlp
        /// </summary>
        /// <param name="execute">action à exécuter.</param>
        /// <param name="canExecute">statut de l'action à exécuter.</param>
        public OVCommandeRoutee(Action execute, Func<bool> canExecute)
        {
            if (execute == null) throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe CommandHlp
        /// </summary>
        /// <param name="execute">action à exécuter.</param>
        public OVCommandeRoutee(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe CommandHlp
        /// </summary>
        /// <param name="execute">action à exécuter.</param>
        /// <param name="canExecute">statut de l'action à exécuter.</param>
        public OVCommandeRoutee(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null) throw new ArgumentNullException("execute");

            executeWithParameter = execute;
            canExecuteWithParameter = canExecute;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (parameter == null && executeWithParameter == null) execute();
            else executeWithParameter(parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (parameter != null) return canExecuteWithParameter == null ? true : canExecuteWithParameter(parameter);
            else return canExecute == null ? true : canExecute();
        }
    }
}
