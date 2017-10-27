using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;
public class XmlCtrl : MonoBehaviour {
   public static XmlCtrl x_xml = null;
   public List<MonsterCardXML> MCxml = new List<MonsterCardXML>();
   public List<MonsterRankXML> MRxml = new List<MonsterRankXML>();
   public List<RankXML> Rxml = new List<RankXML>();
   public List<GachaXML> Gxml = new List<GachaXML>();

	// Use this for initialization
	void Start () {
        x_xml = GameObject.Find("GlobalObject").GetComponent<XmlCtrl>();

        XMLctrl("MonsterXml", 0);
        XMLctrl("MonsterRankXml", 1);
        XMLctrl("RankXml", 2);
        XMLctrl("GachaXml", 3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void XMLctrl(string filename, int flag)
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Xml/" + filename);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);

        XmlNodeList MonsterList = xmldoc.SelectNodes("Monster/tag");
        if (flag == 0)   // monster card Xml
        {
            
            MonsterCardXML MCx = new MonsterCardXML();
            int j = 1;
            foreach (XmlNode node in MonsterList)
            {
                
                MCx.Id = int.Parse(node.SelectSingleNode("Id").InnerText);

                MCx.Name = node.SelectSingleNode("Name").InnerText;
                MCx.Rank = int.Parse(node.SelectSingleNode("Rank").InnerText);
                MCx.Rare = int.Parse(node.SelectSingleNode("Rare").InnerText);
                MCx.Ability1 = int.Parse(node.SelectSingleNode("AT1").InnerText);
                MCx.Lv1ability1value = float.Parse(node.SelectSingleNode("Lv1AT1v").InnerText);
                MCx.AT1Add = float.Parse(node.SelectSingleNode("AT1Add").InnerText);
                MCx.Ability2 = int.Parse(node.SelectSingleNode("AT2").InnerText);
                MCx.Lv1ability2value = float.Parse(node.SelectSingleNode("Lv1AT2v").InnerText);
                MCx.AT2Add = float.Parse(node.SelectSingleNode("AT2Add").InnerText);
                MCx.Ability3 = int.Parse(node.SelectSingleNode("AT3").InnerText);
                MCx.Lv1ability3value = float.Parse(node.SelectSingleNode("Lv1AT3v").InnerText);
                MCx.AT3Add = float.Parse(node.SelectSingleNode("AT3Add").InnerText);

                MCxml.Add(MCx);
               
            }
        }

        else if(flag == 1)     // monster rank xml 
        {
            MonsterRankXML MRx = new MonsterRankXML();

            foreach (XmlNode node in MonsterList)
            {

                MRx.Id = int.Parse(node.SelectSingleNode("Id").InnerText);
                MRx.Rank = int.Parse(node.SelectSingleNode("Rank").InnerText);
                MRx.Level = int.Parse(node.SelectSingleNode("Level").InnerText);
                MRx.Exp = int.Parse(node.SelectSingleNode("Exp").InnerText);
                MRx.Enchant = int.Parse(node.SelectSingleNode("Enchant").InnerText);
                MRx.Sell = int.Parse(node.SelectSingleNode("Sell").InnerText);
                MRx.MaterialExp = int.Parse(node.SelectSingleNode("MaterialExp").InnerText);
                MRx.MixGacha = int.Parse(node.SelectSingleNode("MixGacha").InnerText);

                MRxml.Add(MRx);
                
            }
        }

        else if (flag == 2)     // Rank xml 
        {
          
            RankXML Rx = new RankXML();

            foreach (XmlNode node in MonsterList)
            {
                Rx.Id = int.Parse(node.SelectSingleNode("Id").InnerText);
                Rx.Type = int.Parse(node.SelectSingleNode("Type").InnerText);
                Rx.Value = int.Parse(node.SelectSingleNode("Value").InnerText);
                Rx.Bscore = int.Parse(node.SelectSingleNode("Bscore").InnerText);
                Rx.Bgold = int.Parse(node.SelectSingleNode("Bgold").InnerText);
                Rx.Gacha = int.Parse(node.SelectSingleNode("Gacha").InnerText);

                Rxml.Add(Rx);
            }
        }
        else     // Gacha xml 
        {
            
            GachaXML Gx = new GachaXML();
            foreach (XmlNode node in MonsterList)
            {
                Gx.Id = int.Parse(node.SelectSingleNode("Id").InnerText);
                Gx.Group = int.Parse(node.SelectSingleNode("Group").InnerText);
                Gx.Value = int.Parse(node.SelectSingleNode("Value").InnerText);
                Gx.LookValue = int.Parse(node.SelectSingleNode("LookValue").InnerText);
                Gx.Type = int.Parse(node.SelectSingleNode("Type").InnerText);
                Gx.TypeValue = int.Parse(node.SelectSingleNode("TypeValue").InnerText);

                Gxml.Add(Gx);
            }
        }
    }

    public int calculate(int value1 , int value30 , int valueCurrent)
    {
        int resultc = 0;

        resultc = value1 + ((value30 - value1 ) / 29 * (valueCurrent -1));

        return resultc;
    }

    public float floatCalculate(float value1, float value30, float valueCurrent)
    {
        float resultc = 0;

        resultc = value1 + ((value30 - value1) / 29 * (valueCurrent - 1));

        return resultc;
    }

    public int expCalculate(int value2 , int value30 , int valueCurrent)
    {
        int resultc = 0;

        resultc = value2 + ((value30 - value2 )/28* (valueCurrent - 2  ));

        return resultc;
    }


}
