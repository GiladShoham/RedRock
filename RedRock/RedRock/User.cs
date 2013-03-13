using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Crypto;

namespace RedRock
{
    class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _myId;
        public int MyID
        {
            get { return _myId; }
        }

        public void SetMyID(int value)
        {
            _myId = value;
            this.NotifyPropertyChanged("ID");
        }

        private int _myPhone;
        public int MyPhone
        {
            get { return _myPhone; }
        }

        public void SetMyPhone(int value)
        {
            _myPhone = value;
            this.NotifyPropertyChanged("Phone");
        }

        private string _myName;
        public string MyName
        {
            get { return _myName; }
        }

        public void SetMyName(string value)
        {
            _myName = value;
            this.NotifyPropertyChanged("Name");
        }

        private string _myMail;
        public string MyMail
        {
            get { return _myMail; }
        }

        public void SetMyMail(string value)
        {
            _myMail = value;
            this.NotifyPropertyChanged("Mail");
        }

        private string _myKey;
        public string MyKey
        {
            get { return _myKey; }
        }

        public void SetMyKey(string value)
        {
            _myKey = value;
            this.NotifyPropertyChanged("Key");
        }

       
       
       

        public User()
        {
        }

        public User(int myID, String myName, int myPhone, string myMail)
        {
            this.SetMyID(myID);
            this.SetMyPhone(myPhone);
            this.SetMyMail(myMail);
            this.SetMyName(myName);
            this.SetMyKey(EncDec.CreateKey(8));
        }

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }

}
