﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Web.UI;

namespace GeoSI.Website.Common
{
    public class LoginGeo : Page
    {
        public LoginGeo()
        {
        }

        public string Hash(string Password)
        {
            string chaine = "";
            string reformulation = "asm" + Password + "tda";
            string hashOfInput = FormsAuthentication.HashPasswordForStoringInConfigFile(reformulation, "MD5");
            string chaine1 = hashOfInput.Substring(0, 12);
            string chaine2 = hashOfInput.Substring(0, 13);
            string chaine3 = hashOfInput.Substring(0, 7);
            chaine = chaine3 + chaine1 + chaine2;
            return chaine;
        }
        //Récuperation des droits d'accès
        protected List<string> getHabilitation(string _Requette)
        {
            List<string> _listHabilitation=new List<string>();
            try
            {
                SqlConnection cnx = _Global.CurrentConnection;

                SqlCommand cmd = new SqlCommand(_Requette, cnx);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _listHabilitation.Add(dr.GetString(0).ToString());
                }
                dr.Close();
                cmd.Dispose();
                //cnx.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return _listHabilitation;
        }

        //Authentification
        public List<string> Authentification(string _req)
        {
            List<string> ClientId = new List<string>();


            SqlConnection cnx = _Global.CurrentConnection;
            SqlCommand cmd = new SqlCommand(_req, cnx);
            SqlDataReader resulat = cmd.ExecuteReader();

            while (resulat.Read())
            {
                ClientId.Add(resulat[0].ToString());
                ClientId.Add(resulat[1].ToString());
                ClientId.Add(resulat[2].ToString());
            }
            resulat.Close();
            cmd.Dispose();
           // cnx.Close();
            return ClientId;
        }

        
    }
}