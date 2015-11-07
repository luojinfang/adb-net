﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ADB_Helper
{
    public class NavBar
    {
        #region "Static Methods"
        public static List<NavBar> GetAllAvailable()
        {
            List<NavBar> list = new List<NavBar>();
            NavBar curr = null;

            foreach (string item in Directory.EnumerateFiles("./media", "nav_*.xml"))
            {
                using (XmlReader reader = XmlReader.Create(item))
                {
                    while(reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "navbar":
                                    curr = new NavBar();
                                    break;
                                case "id":
                                    reader.Read();
                                    curr.id = reader.Value;
                                    break;
                                case "name":
                                    reader.Read();
                                    curr.name = reader.Value;
                                    break;
                                case "btn_recent":
                                    reader.Read();
                                    curr.buttonRecent = Image.FromFile("./media/" + reader.Value);
                                    break;
                                case "btn_home":
                                    reader.Read();
                                    curr.buttonHome = Image.FromFile("./media/" + reader.Value);
                                    break;
                                case "btn_back":
                                    reader.Read();
                                    curr.buttonBack = Image.FromFile("./media/" + reader.Value);
                                    break;
                                case "bgcolor":
                                    reader.Read();
                                    curr.bgColor = ColorTranslator.FromHtml(reader.Value);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    list.Add(curr);
                }
            }

            return list;
        }
        #endregion

        public string id, name;
        public Image buttonHome, buttonBack, buttonRecent;
        public Color bgColor;

        //public NavBar(string id, string name, Image btnHome, Image btnBack, Image btnRecent)
        //{
        //    this.buttonHome = btnHome;
        //    this.buttonBack = btnBack;
        //    this.buttonRecent = btnRecent;
        //    this.id = id;
        //    this.name = name;
        //}
    }
}
