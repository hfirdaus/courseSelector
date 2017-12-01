﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CPSC481_Prototype
{
    class Messages : INotifyCollectionChanged
    {

        public static ObservableCollection<Message> messages = new ObservableCollection<Message>();

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public static void AddMessage(string text)
        {
            MainWindow.instance.AddMessage(new Message(text));
        }

        public static void AddMessage(string text, ProgAction action)
        {
            Message msg = new Message(text);
            msg.clearMsg = action;
            //messages.Add(msg);
            MainWindow.instance.AddMessage(msg);
        }

        public static void AddUndoMessage(string text, Action action)
        {
            Message msg = new Message(text);
            msg.clearMsg = new UndoAction(action);
            //messages.Add(msg);
            MainWindow.instance.AddMessage(msg);
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

        public static void ClearMessages()
        {
            List<Message> toRemove = new List<Message>();
            foreach(Message msg in messages)
            {
                //if (msg.Opac == 0.0)
                    toRemove.Add(msg);
            }
            foreach (Message m in toRemove) messages.Remove(m);
            Console.Out.WriteLine("Number of messages: " + messages.Count);
        }
    }

    public class Message
    {
        public string text { get; set; }
        public ProgAction clearMsg { get; set; }

        public Message(string message)
        {
            this.text = message;
            
        }

        /*
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
        */
    }
    public abstract class ProgAction
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

    public class UndoAction : ProgAction
    {
        public UndoAction(Action action) : base(action) { }
    }

    public class RedoAction : ProgAction
    {
        public RedoAction(Action action) : base(action) { }
    }

}
