using OSRabbitMQPublisher;
using OSRabbitMQPublisher.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RabbitMQPublisherClientExample
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _rabbitMQUrl;
        public string RabbitMQUrl
        {
            get { return _rabbitMQUrl; }
            set { _rabbitMQUrl = value; }
        }
        private int _rabbitMQPort;
        public int RabbitMQPort
        {
            get { return _rabbitMQPort; }
            set { _rabbitMQPort = value; }
        }

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get { return _loginCommand ?? (_loginCommand = new CommandHandler(() => ConnectAction(), _canExecute)); }
        }

        private bool _canExecute;

        private IMQPublisher _rabbitMQPublisher;

        public MainWindowViewModel()
        {
            _rabbitMQUrl = "localhost";
            _canExecute = true;
        }
        public void ConnectAction()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
    
        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
