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
            using (var reader = new System.IO.StreamReader(@"C:\Users\Arno\Documents\GitHub\NBAStats\ConvertCSV\ConvertCSV\laatsteStuks.csv"))
            {
                reader.ReadLine(); //ignore first line
                reader.ReadLine();


                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] parts = line.Split(',');

                    CollegeStats c = new CollegeStats();

                    //Rk,Player,Class,Season,Pos,School,Conf,G,MP,FG,FGA,2P,2PA,3P,3PA,FT,FTA,ORB,DRB,TRB,AST,STL,BLK,TOV,PF,PTS

                    c.Player = parts[1];
                    c.Class = parts[2];
                    c.Season = parts[3];
                    c.pos = parts[4];
                    c.school = parts[5];

                    c.Games = (parts[7] != "") ? int.Parse(parts[7]) : 0;
                    c.Mp = (parts[8] != "") ? int.Parse(parts[8]) : 0;
                    c.FG = int.Parse(parts[9]);
                    c.FGA = int.Parse(parts[10]);
                    c.twop = (parts[11] != "") ? int.Parse(parts[11]) : 0;
                    c.twoPA = (parts[12] != "") ? int.Parse(parts[12]) : 0;
                    c.treeP = (parts[13] != "") ? int.Parse(parts[13]) : 0;
                    c.treePA = (parts[14] != "") ? int.Parse(parts[14]) : 0;
                    c.FT = int.Parse(parts[15]);
                    c.FTA = int.Parse(parts[16]);
                    c.ORB = (parts[17] != "") ? int.Parse(parts[17]) : 0;
                    c.DRB = (parts[18] != "") ? int.Parse(parts[18]) : 0;
                    c.TRB = int.Parse(parts[19]);
                    c.AST = int.Parse(parts[20]);
                    c.STL = int.Parse(parts[21]);
                    c.BLK = int.Parse(parts[22]);
                    c.TOV = (parts[23] != "") ? int.Parse(parts[23]) : 0;
                    c.PF = (parts[24] != "") ? int.Parse(parts[24]) : 0;
                    c.PTS = int.Parse(parts[25]);

                    collegeStatsList.Add(c);

                    line = reader.ReadLine();
                }
            }//end read coll stats


            using (var reader = new System.IO.StreamReader(@"C:\Users\Arno\Documents\GitHub\NBAStats\ConvertCSV\ConvertCSV\CollegeStatsFirst1000.csv"))
            {
                reader.ReadLine(); //ignore first line
                reader.ReadLine();


                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] parts = line.Split(',');

                    CollegeStats c = new CollegeStats();

                    //Rk,Player,Class,Season,Pos,School,Conf,G,MP,FG,FGA,2P,2PA,3P,3PA,FT,FTA,ORB,DRB,TRB,AST,STL,BLK,TOV,PF,PTS

                    c.Player = parts[1];
                    c.Class = parts[2];
                    c.Season = parts[3];
                    c.pos = parts[4];
                    c.school = parts[5];

                    c.Games = int.Parse(parts[7]);
                    c.Mp = (parts[8] != "") ? int.Parse(parts[8]) : 0;
                    c.FG = int.Parse(parts[9]);
                    c.FGA = int.Parse(parts[10]);
                    c.twop = int.Parse(parts[11]);
                    c.twoPA = int.Parse(parts[12]);
                    c.treeP = int.Parse(parts[13]);
                    c.treePA = int.Parse(parts[14]);
                    c.FT = int.Parse(parts[15]);
                    c.FTA = int.Parse(parts[16]);
                    c.ORB = (parts[17] != "") ? int.Parse(parts[17]) : 0;
                    c.DRB = (parts[18] != "") ? int.Parse(parts[18]) : 0;
                    c.TRB = int.Parse(parts[19]);
                    c.AST = int.Parse(parts[20]);
                    c.STL = int.Parse(parts[21]);
                    c.BLK = int.Parse(parts[22]);
                    c.TOV = (parts[23] != "") ? int.Parse(parts[23]) : 0;
                    c.PF = (parts[24] != "") ? int.Parse(parts[24]) : 0;
                    c.PTS = int.Parse(parts[25]);

                    collegeStatsList.Add(c);

                    line = reader.ReadLine();
                }
            }//end read coll2stats


            Console.WriteLine("Combining seasons");
            //add season stats from players;
            for (int i = collegeStatsList.Count - 1; i >= 0; i--)
            {
                List<int> lstFoundi = new List<int>();
                bool inList = false;
                for (int j = 0; j < freshCollegeList.Count; j++)
                {
                    if (collegeStatsList[i].Player == freshCollegeList[j].Player) inList = true;

                }

                if (!inList)
                {
                    for (int j = 0; j < collegeStatsList.Count; j++)
                    {
                        if (collegeStatsList[i].Player == collegeStatsList[j].Player
                            && collegeStatsList[i].Season != collegeStatsList[j].Season)
                        {
                            lstFoundi.Add(j);
                        }
                    }

                    CollegeStats fCollegePlayer = collegeStatsList[i];
                    int s = lstFoundi.Count + 1;
                    fCollegePlayer.Season = s.ToString();


                    foreach (int k in lstFoundi)
                    {
                        fCollegePlayer.Games += collegeStatsList[k].Games;
                        fCollegePlayer.Mp += collegeStatsList[k].Mp;
                        fCollegePlayer.FG += collegeStatsList[k].FG;
                        fCollegePlayer.twop += collegeStatsList[k].twop;
                        fCollegePlayer.twoPA += collegeStatsList[k].twoPA;
                        fCollegePlayer.treeP += collegeStatsList[k].treeP;
                        fCollegePlayer.treePA += collegeStatsList[k].treePA;
                        fCollegePlayer.FT += collegeStatsList[k].FT;
                        fCollegePlayer.FTA += collegeStatsList[k].FTA;
                        fCollegePlayer.DRB += collegeStatsList[k].DRB;
                        fCollegePlayer.ORB += collegeStatsList[k].ORB;
                        fCollegePlayer.TRB += collegeStatsList[k].TRB;
                        fCollegePlayer.AST += collegeStatsList[k].AST;
                        fCollegePlayer.STL += collegeStatsList[k].STL;
                        fCollegePlayer.BLK += collegeStatsList[k].BLK;
                        fCollegePlayer.TOV += collegeStatsList[k].TOV;
                        fCollegePlayer.PF += collegeStatsList[k].PF;
                        fCollegePlayer.PTS += collegeStatsList[k].PTS;
                    }
                    freshCollegeList.Add(fCollegePlayer);
                }
            }


            Console.WriteLine("Searching picked players");
            //search if player is picked
            foreach (CollegeStats pl in freshCollegeList)
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

            Console.WriteLine("Writing file");
            //write csv
            using (var writer = new StreamWriter(@"C:\Users\Arno\Documents\GitHub\NBAStats\ConvertCSV\ConvertCSV\Output.csv"))
            {
                //headers
                writer.WriteLine("Player, Class, Season, Position, School, Games, Minutes, FG, FGA, 2P, 2PA, 3P, 3PA" +
                    "FT, FTA, ORB, DRB, TRB, AST, STL, BLK, TOV, PF, PTS, Picked");




                foreach (CollegeStats s in freshCollegeList)
                {
                    int pick = 0;
                    if (s.Picked) pick = 1;
                    string line = s.Player + ","
                        + s.Class + ","
                        + s.Season + ","
                        + s.pos + ","
                        + s.school + ","
                        + s.Games + ","
                        + s.Mp + ","
                        + s.FG + ","
                        + s.FGA + ","
                        + s.twop + ","
                        + s.twoPA + ","
                        + s.treeP + ","
                        + s.treePA + ","
                        + s.FT + ","
                        + s.FTA + ","
                        + s.ORB + ","
                        + s.DRB + ","
                        + s.TRB + ","
                        + s.AST + ","
                        + s.STL + ","
                        + s.BLK + ","
                        + s.TOV + ","
                        + s.PF + ","
                        + s.PTS + ","
                        + pick;
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
        public String Season;
        public string pos;
        public string school;
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
        public bool Picked;
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
