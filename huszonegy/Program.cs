/*
 * Created by SharpDevelop.
 * User: zsolt
 * Date: 2018. 09. 28.
 * Time: 20:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace huszonegy
{
	class Program
	{
		static void kirajzol(string lap,int x,int y){
			Console.SetCursorPosition(x,y);
			Console.BackgroundColor=ConsoleColor.White;
			if (lap[0]=='♥' || lap[0]=='♦') {
				Console.ForegroundColor=ConsoleColor.Red;
			};
			if (lap[0]=='♣' || lap[0]=='♠') {
				Console.ForegroundColor=ConsoleColor.Black;
			};
			Console.WriteLine("┌────────┐");
			Console.SetCursorPosition(x,y+1);
			if (lap[1]=='1') {
				Console.WriteLine("│"+lap[1]+lap[2]+"      │");
			} else {
				Console.WriteLine("│"+lap[1]+"       │");
			}
			Console.SetCursorPosition(x,y+2);
			Console.WriteLine("│"+lap[0]+"       │");
			Console.SetCursorPosition(x,y+3);
			Console.WriteLine("│        │");
			Console.SetCursorPosition(x,y+4);
			Console.WriteLine("│        │");
			Console.SetCursorPosition(x,y+5);
			Console.WriteLine("│       "+lap[0]+"│");
			Console.SetCursorPosition(x,y+6);
			if (lap[1]=='1') {
				Console.WriteLine("│      "+lap[1]+lap[2]+"│");
			} else {
				Console.WriteLine("│       "+lap[1]+"│");
			}
			Console.SetCursorPosition(x,y+7);
			Console.WriteLine("└────────┘");
			Console.BackgroundColor=ConsoleColor.Black;
			Console.ForegroundColor=ConsoleColor.White;
		}
		
		static int lapszamol(List<string> eddigi_lapok){
			int osszeg=0;
			int aszok_szama=0;
			//megszámolja az ászokat
			for (int i = 0; i < eddigi_lapok.Count; i++) {
				if(eddigi_lapok[i][1]=='A') aszok_szama++;
			}
			
			for (int i = 0; i < eddigi_lapok.Count; i++) {
				switch(eddigi_lapok[i][1]){
					case '2': osszeg+=2;break;	
					case '3': osszeg+=3;break;
					case '4': osszeg+=4;break;
					case '5': osszeg+=5;break;
					case '6': osszeg+=6;break;
					case '7': osszeg+=7;break;
					case '8': osszeg+=8;break;
					case '9': osszeg+=9;break;
					case '1': osszeg+=10;break;
					case 'J': osszeg+=10;break;
					case 'Q': osszeg+=10;break;
					case 'K': osszeg+=10;break;
					case 'A': osszeg+=11;break;
				}
				
			}
			//ászokat 11-nek számolva összeadja a paklit
			
			//kis trükközés:
			while(aszok_szama>0 && osszeg>21){
				aszok_szama--;
				osszeg-=10;
			}	
			return osszeg;
		}
		
		public static void Main(string[] args)
		{
			Console.OutputEncoding=Encoding.UTF8;
			string[] szamok={"A","2","3","4","5","6","7","8","9","10","J","Q","K"};
			char[] szinek={'♥','♦','♣','♠'};
			//egy pakli
			List<string> pakli=new List<string>();
			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < 13; j++) {
					pakli.Add(szinek[i]+szamok[j]);
					//kirajzol(szinek[i]+szamok[j],i*10,j*9);
				}
			}
			
			int oszto_ertek=0, jatekos_ertek=0;
			List<string> oszto_lapjai=new List<string>();
			List<string> jatekos_lapjai=new List<string>();
			
			//******************** Osztás *********
			
			Console.WriteLine("Játékos                                            Osztó");
			
			int lapok_szama=52;
			Random rand=new Random();
			int osztott_lap=rand.Next(lapok_szama);
			jatekos_lapjai.Add(pakli[osztott_lap]);
			pakli.RemoveAt(osztott_lap);
			lapok_szama--;
			kirajzol(jatekos_lapjai[0],0,2);
			Thread.Sleep(100);
			
			osztott_lap=rand.Next(lapok_szama);
			oszto_lapjai.Add(pakli[osztott_lap]);
			pakli.RemoveAt(osztott_lap);
			lapok_szama--;
			kirajzol(oszto_lapjai[0],50,2);
			Thread.Sleep(100);
			
			osztott_lap=rand.Next(lapok_szama);
			jatekos_lapjai.Add(pakli[osztott_lap]);
			pakli.RemoveAt(osztott_lap);
			lapok_szama--;
			kirajzol(jatekos_lapjai[1],10,2);
			Thread.Sleep(100);
			
			osztott_lap=rand.Next(lapok_szama);
			oszto_lapjai.Add(pakli[osztott_lap]);
			pakli.RemoveAt(osztott_lap);
			lapok_szama--;
			kirajzol("  ",60,2);
			Thread.Sleep(100);
			//************************** Játékos lapot kérhet ***************
			jatekos_ertek=lapszamol(jatekos_lapjai);
			Console.SetCursorPosition(30,10);
			Console.Write("sum:"+jatekos_ertek);
			int meglap=0;
			Console.SetCursorPosition(0,10);
			Console.Write("Kérsz még lapot?(i)");
			while ((Console.ReadKey(true).Key == ConsoleKey.I) && jatekos_ertek<22) {
				meglap++;
				osztott_lap=rand.Next(lapok_szama);
				jatekos_lapjai.Add(pakli[osztott_lap]);
				kirajzol(pakli[osztott_lap],-10+meglap*10,12);
				pakli.RemoveAt(osztott_lap);
				lapok_szama--;
				Thread.Sleep(100);
				jatekos_ertek=lapszamol(jatekos_lapjai);
				Console.SetCursorPosition(30,10);
				Console.Write("sum:"+jatekos_ertek+"  ");
				if (jatekos_ertek>21) {
					Console.Write("Sok!!");
				}
			}
			//********************* Osztón van a sor ****************
			kirajzol(oszto_lapjai[1],60,2);
			Thread.Sleep(100);
			oszto_ertek=lapszamol(oszto_lapjai);
			Console.SetCursorPosition(60,10);
			Console.Write("sum:"+oszto_ertek+"  ");
			oszto_ertek=lapszamol(oszto_lapjai);
			
			while(oszto_ertek<=16 || (oszto_ertek<jatekos_ertek && jatekos_ertek<=21)){
				meglap++;
				osztott_lap=rand.Next(lapok_szama);
				oszto_lapjai.Add(pakli[osztott_lap]);
				kirajzol(pakli[osztott_lap],meglap*10+30,12);
				pakli.RemoveAt(osztott_lap);
				lapok_szama--;
				Thread.Sleep(800);
				oszto_ertek=lapszamol(oszto_lapjai);
				Console.SetCursorPosition(60,10);
				Console.Write("sum:"+oszto_ertek+"  ");
				if (oszto_ertek>21) {
					Console.Write("Sok!!");
				}	
			}
			Console.WriteLine();
			//*************** Kiértékelés ***************************
			if ((jatekos_ertek>21 && oszto_ertek>21) || jatekos_ertek==oszto_ertek) {
				Console.ForegroundColor=ConsoleColor.Blue;
				Console.Write("                                   Döntetlen!");
			} else if (oszto_ertek>21 || (jatekos_ertek<=21 && jatekos_ertek>oszto_ertek )){
				Console.ForegroundColor=ConsoleColor.Green;
				Console.Write("                                   Nyertél!");
			} else {
				Console.ForegroundColor=ConsoleColor.Red;
				Console.Write("                                   Vesztettél!");
			}
			
			Console.ForegroundColor=ConsoleColor.White;
			Console.Write("  Press any key to continue . . . ");
			Console.ReadKey(true);
			Console.SetCursorPosition(0,20);
			Console.Write("Akarod látni milyen lapok maradtak a pakliban?(i)");
			if (Console.ReadKey(true).Key == ConsoleKey.I) {
				int sor=21, oszlop=0;
			foreach (var element in pakli) {
				kirajzol(element,oszlop,sor);					
				oszlop+=5;
				if (oszlop>70) {
					oszlop=0;
					sor+=10;
				}
			}
			}
			
			Console.Write("  Press any key to continue . . . ");
			Console.ReadKey(true);
			
		}
	}
}