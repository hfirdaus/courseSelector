using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPSC481_Prototype
{
    class Messages : INotifyCollectionChanged
    {

        public static ObservableCollection<Message> messages = new ObservableCollection<Message>();

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public static void AddMessage(string text)
        {
            messages.Add(new Message(text));
        }

        public static void AddMessage(string text, ProgAction action)
        {

        }

        public static void AddUndoMessage(string text, Action action)
        {

        }
        
        public static void AddUndoMessage(string text, UndoAction action)
        {

        }

        public static void AddRedoMessage(string text, Action action)
        {

        }

        public static void AddRedoMessage(string text, RedoAction action)
        {

        }

        public static void RemoveMessage(Message message)
        {
            messages.Remove(message);
        }
    }

    class Message
    {
        public string text { get; set; }
        public ICommand clear;

        public Message(string message)
        {
            this.text = message;
            clear = new MessageClear(this);
        }

        class MessageClear : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private Message message;
            public MessageClear(Message message)
            {
                this.message = message;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                Messages.RemoveMessage(message);
            }
        }
    }
    abstract class ProgAction
    {

        public ProgAction(Action action)
        {
            this.action = action;
        }

        private Action action;

        public void run()
        {
            action();
        }
    }

    class UndoAction : ProgAction
    {
        public UndoAction(Action action) : base(action) { }
    }

    class RedoAction : ProgAction
    {
        public RedoAction(Action action) : base(action) { }
    }

}
