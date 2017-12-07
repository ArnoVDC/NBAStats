using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConvertCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Working");

            List<nbaStat> nbaStatList = new List<nbaStat>();

            List<CollegeStats> collegeStatsList = new List<CollegeStats>();
            List<AllStats> allStatsList = new List<AllStats>();
            List<CollegeStats> freshCollegeList = new List<CollegeStats>();
            List<String> _schools = new List<string>();

            int PlayersNotFound = 0;

            Console.WriteLine("Reading NBA Stats");
            //reading first list
            using (var reader = new System.IO.StreamReader(@"C:\Users\Arno\Documents\GitHub\NBAStats\ConvertCSV\ConvertCSV\NBAStats.csv"))
            {
                reader.ReadLine(); //ignore first line
                reader.ReadLine();


                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] parts = line.Split(',');

                    nbaStat nba = new nbaStat();
                    nba.Year = int.Parse(parts[1]);
                    nba.Lg = parts[2];
                    nba.Rd = int.Parse(parts[3]);
                    nba.Pk = int.Parse(parts[4]);
                    nba.Tm = parts[5];
                    nba.Player = parts[6];
                    nba.age = parts[7];
                    nba.pos = parts[8];
                    nba.College = parts[10];

                    nbaStatList.Add(nba);

                    line = reader.ReadLine();
                }
            }//end read nba stats

            Console.WriteLine("Reading College Stats");
            using (var reader = new System.IO.StreamReader(@"C:\Users\Arno\Documents\GitHub\NBAStats\ConvertCSV\ConvertCSV\College.csv"))
            {
                reader.ReadLine(); //ignore first line
                reader.ReadLine();


                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] parts = line.Split(',');

                    CollegeStats c = new CollegeStats();
                    c.Player = parts[1];
                    int s = int.Parse(parts[3]) - int.Parse(parts[2]) + 1;
                    c.Season = s;
                    c.school = parts[4];
                    //Conf,G,MP,FG,FGA,2P,2PA,3P,3PA,FT,FTA,ORB,DRB,TRB,AST,STL,BLK,TOV,PF,PTS
                    c.Class = parts[5];
                    c.Games = int.Parse(parts[6]) / s;
                    //c.Mp = int.Parse(parts[7]);

                    c.FG = int.Parse(parts[8]) / s;
                    c.FGA = int.Parse(parts[9]) / s;
                    c.FG = Math.Round(c.FG / c.FGA * 100, 0);
                    if (c.FGA == 0) c.FG = 0;

                    c.twop = int.Parse(parts[10]) / s;
                    c.twoPA = int.Parse(parts[11]) / s;
                    c.twop = Math.Round(c.twop / c.twoPA * 100, 0);
                    if (c.twoPA == 0) c.twop = 0;

                    c.treeP = int.Parse(parts[12]) / s;
                    c.treePA = int.Parse(parts[13]) / s;
                    c.treeP = Math.Round(c.treeP / c.treePA * 100, 0);
                    if (c.treePA == 0) c.treeP = 0;

                    c.FT = int.Parse(parts[14]) / s;
                    c.FTA = int.Parse(parts[15]) / s;
                    c.FT = Math.Round(c.FT / c.FTA * 100, 0);
                    if (c.FTA == 0) c.FT = 0;
                    //orb and drb do not count
                    c.TRB = int.Parse(parts[18]) / s;
                    c.AST = int.Parse(parts[19]) / s;
                    c.STL = int.Parse(parts[20]) / s;
                    c.BLK = int.Parse(parts[21]) / s;
                    //c.TOV = int.Parse(parts[22]);
                    //c.PF = int.Parse(parts[23]);
                    c.PTS = int.Parse(parts[24]) / s;

                    if (c.Player == @"Omar Cooper\omar-cooper-1")
                    {
                        Console.WriteLine("here");
                    }
                    collegeStatsList.Add(c);
                    line = reader.ReadLine();
                }
            }//end read college stats

            Console.WriteLine("getting schools");
            foreach (CollegeStats pl in collegeStatsList)
            {
                bool f = false;
                for (int i = 0; i < _schools.Count; i++)
                {
                    if (pl.school == _schools[i])
                    {
                        f = true;
                        pl.schoolId = i;
                    }
                }

                if (!f) {
                    _schools.Add(pl.school);
                    pl.schoolId = _schools.Count - 1;
                        
                        };
            }


            Console.WriteLine("Searching picked players");
            //search if player is picked
            foreach (CollegeStats pl in collegeStatsList)
            {
                pl.Picked = false;
                string[] name = pl.Player.Split('\\');
                foreach (nbaStat nbaPlayer in nbaStatList)
                {
                    string[] name2 = nbaPlayer.Player.Split('\\');
                    if (name[0] == name2[0])
                    {
                        //player in nba
                        pl.Picked = true;
                    }
                }
            }
            string addTitle = ",";
            foreach(String s in _schools)
            {
                addTitle += s + ",";
            }
            addTitle.Substring(0, addTitle.Length -1);


            Console.WriteLine("Writing file");
            //write csv
            using (var writer = new StreamWriter(@"C:\Users\Arno\Documents\GitHub\NBAStats\ConvertCSV\ConvertCSV\Output.csv"))
            {
                //headers
                writer.WriteLine("Player,Class,Season,Position,School,Games,FG,2P,3P," +
                    "FT,TRB,AST,STL,BLK,PTS" + addTitle + ",Picked");



                foreach (CollegeStats s in collegeStatsList)
                {
                    int pick = 0;
                    if (s.Picked) pick = 1;
                    string line = s.Player + ","
                        + s.Class + ","
                        + s.Season + ","
                        + s.pos + ","
                        + s.school + ","
                        + s.Games + ","

                        + s.FG + ","

                        + s.twop + ","

                        + s.treeP + ","

                        + s.FT + ","


                        + s.TRB + ","
                        + s.AST + ","
                        + s.STL + ","
                        + s.BLK + ","

                        + s.PTS;

                    for(int i = 0; i< _schools.Count; i++)
                    {
                        if (s.schoolId == i) line += ",1";
                        else line += ",0";
                    }

                    line += "," + pick;
                    writer.WriteLine(line);
                    writer.Flush();
                }
            }


            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }


    class AllStats
    {
        public int Year;
        public string Lg;
        public int Rd;
        public int Pk;
        public string Tm;
        public string Player;
        public string age;
        public string pos;
        public string College;
        public string Class;
        public int Season;
        public int Games;
        public int Mp;
        public int FG;
        public int FGA;
        public int twop;
        public int twoPA;
        public int treeP;
        public int treePA;
        public int FT;
        public int FTA;
        public int ORB;
        public int DRB;
        public int TRB;
        public int AST;
        public int STL;
        public int BLK;
        public int TOV;
        public int PF;
        public int PTS;
    }

    class CollegeStats
    {
        public string Player;
        public string Class;
        public int Season;
        public string pos;
        public string school;
        public double Games;
        public double Mp;
        public double FG;
        public double FGA;
        public double twop;
        public double twoPA;
        public double treeP;
        public double treePA;
        public double FT;
        public double FTA;
        public double ORB;
        public double DRB;
        public double TRB;
        public double AST;
        public double STL;
        public double BLK;
        public double TOV;
        public double PF;
        public double PTS;
        public bool Picked;
        public int schoolId;
    }

    class nbaStat
    {
        public int Year;
        public string Lg;
        public int Rd;
        public int Pk;
        public String Tm;
        public string Player;
        public string age;
        public string pos;
        public string College;
    }
}
