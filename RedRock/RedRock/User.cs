using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Crypto;

namespace RedRock
{
    [Serializable()]
    public class User : INotifyPropertyChanged, ISerializable, IEquatable<User>
    {
        

        public event PropertyChangedEventHandler PropertyChanged;


        private int _myID;

        public int MyID
        {
            get { return _myID; }
            set
            {
                _myID = value;
                this.NotifyPropertyChanged("MyID");
            }
        }


        private string _myPhone;

        public string MyPhone
        {
            get { return _myPhone; }
            set
            {
                _myPhone = value;
                this.NotifyPropertyChanged("MyPhone");
            }
        }


        private string _myName;

        public string MyName
        {
            get { return _myName; }
            set
            {
                _myName = value;
                this.NotifyPropertyChanged("MyName");
            }
        }


        private string _myMail;

        public string MyMail
        {
            get { return _myMail; }
            set
            {
                _myMail = value;
                this.NotifyPropertyChanged("MyMail");
            }
        }


        private string _myKey;

        public string MyKey
        {
            get { return _myKey; }
            set
            {
                _myKey = value;
                this.NotifyPropertyChanged("MyKey");
            }
        }


        public User()
        {
        }

        public User(int myID, String myName, string myPhone, string myMail)
        {
            MyID = (myID);
            MyPhone = (myPhone);
            MyMail = (myMail);
            MyName = (myName);
            MyKey = (EncDec.CreateKey(8));
        }

        public User(int myID, String myName, string myPhone, string myMail, string myKey)
        {
            MyID = (myID);
            MyPhone = (myPhone);
            MyMail = (myMail);
            MyName = (myName);
            MyKey = myKey;
        }

        public User(SerializationInfo info, StreamingContext ctxt)
        {
            MyID = (int) info.GetValue("Id", typeof(int));
            MyPhone = (string) info.GetValue("Phone",typeof(string));
            MyMail =(string) info.GetValue("Mail", typeof(string));
            MyName = (string)info.GetValue("Name", typeof(string));
            MyKey = (string)info.GetValue("Key", typeof(string));

            
        }

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Id", this.MyID);
            info.AddValue("Name", this.MyName);
            info.AddValue("Phone", this.MyPhone);
            info.AddValue("Mail", this.MyMail);
            info.AddValue("Key", this.MyKey);
        }

        public bool Equals(User other)
        {
            // whatever your custom equality logic is
            return other.MyID == MyID &&
                   other.MyName == MyName &&
                   other.MyPhone == MyPhone &&
                   other.MyMail == MyMail &&
                   other.MyKey == MyKey;

        }
    }
}