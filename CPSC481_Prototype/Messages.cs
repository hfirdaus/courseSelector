﻿using System;


namespace CPSC481_Prototype
{
    class Messages
    {

        public static void AddMessage(string text)
        {
            MainWindow.instance.AddMessage(new Message(text));
        }

        public static void AddMessage(string text, ProgAction action)
        {
            Message msg = new Message(text);
            msg.clearMsg = action;
            MainWindow.instance.AddMessage(msg);
        }

        public static void AddUndoMessage(string text, Action action)
        {
            Message msg = new Message(text);
            msg.clearMsg = new UndoAction(action);
            MainWindow.instance.AddMessage(msg);
        }
        
        public static void AddUndoMessage(string text, UndoAction action)
        {
            Message msg = new Message(text);
            msg.clearMsg = action;
            MainWindow.instance.AddMessage(msg);
        }

        public static void AddRedoMessage(string text, Action action)
        {
            Message msg = new Message(text);
            msg.clearMsg = new RedoAction(action);
            MainWindow.instance.AddMessage(msg);
        }

        public static void AddRedoMessage(string text, RedoAction action)
        {
            Message msg = new Message(text);
            msg.clearMsg = action;
            MainWindow.instance.AddMessage(msg);
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
