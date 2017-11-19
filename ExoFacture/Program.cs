using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoFacture
{
    class Program
    {
        static int Saisie(ref string[] tabDes, ref double[] tabPu, ref int[] tabQ, ref double[] tabMont)
        {
            int i = 0;
            string reponse = "oui";

            while ((reponse.ToLower() == "oui" || reponse.ToLower() == "o") && i <= 20)
            {
                Console.WriteLine("Entrez la designation du produit");
                tabDes[i] = Console.ReadLine();

                Console.WriteLine("Entrez le prix unitaire du produit");
                tabPu[i] = double.Parse(Console.ReadLine());

                Console.WriteLine("Veuillez entrer la quantitée");
                tabQ[i] = Int32.Parse(Console.ReadLine());

                tabMont[i] = tabPu[i] * tabQ[i];

                Console.WriteLine("Voulez-vous entrer un autre produit ? (OUI / NON)");
                reponse = Console.ReadLine();

                i++;
            }

            return i;
        }

        static void coordonnées(out string nom, out string prenom, out string adresse, out string ville, out int cp)
        {
            Console.WriteLine("Veuillez entrer votre nom");
            nom = Console.ReadLine();

            Console.WriteLine("Veuillez entrer votre prénom");
            prenom = Console.ReadLine();

            Console.WriteLine("Veuillez entrer votre adresse");
            adresse = Console.ReadLine();

            Console.WriteLine("Veuillez entrer votre code postal");
            cp = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Veuillez entrer votre ville");
            ville = Console.ReadLine();

            Console.WriteLine();
        }

        static void afficheDebutFacture(string prenom, string nom, string adresse, int cp, string ville)
        {
            Console.WriteLine("\t Facture ");
            Console.WriteLine("\t\t " + prenom + " " + nom);
            Console.WriteLine("\t\t " + adresse);
            Console.WriteLine("\t\t " + cp + " " + ville + "\n");

            Console.WriteLine("Designation \t PU \t Q \t Montant\n");
        }

        static double afficheTableaux(int nbProduits, string[] tabDes, double[] tabPu, double[] tabMont, int[] tabQ)
        {
            double totalHt = 0;

            for (int i = 0; i < nbProduits; i++)
            {
                Console.WriteLine(tabDes[i] + "\t\t " + tabPu[i] + "\t" + tabQ[i] + "\t" + Math.Round(tabMont[i], 2));
                totalHt += (double)tabPu[i] * tabQ[i];
            }

            return totalHt;
        }

        static double savoirRemise(double totalHt, double tauxRemise)
        {
            bool remise = false;
            double net = 0;

            if (totalHt > 400)
            {
                remise = true;
                net = totalHt - totalHt * tauxRemise;
            }

            Console.WriteLine("\nTotal HT : \t\t" + Math.Round(totalHt, 2) + " euros");

            if (remise)
            {
                Console.WriteLine("Remise de 10% : \t" + Math.Round((totalHt * tauxRemise), 2) + " euros");
                Console.WriteLine("Net : \t\t\t" + Math.Round(net, 2) + " euros");
                totalHt = net;
            }

            return totalHt;
        }

        static void affichage(string nom, string prenom, string adresse, string ville, int cp,
                                string[] tabDes, double[] tabPu, double[] tabMont, int[] tabQ, int nbProduits)
        {
            double totalHt = 0, tva = 0.20, tauxRemise = 0.1, totalTTC;

            afficheDebutFacture(prenom, nom, adresse, cp, ville);

            totalHt = afficheTableaux(nbProduits, tabDes, tabPu, tabMont, tabQ);

            totalHt = savoirRemise(totalHt, tauxRemise);

            Console.WriteLine("TVA 20% : \t\t" + Math.Round((totalHt * tva), 2) + " euros");

            totalTTC = totalHt + (totalHt * tva);

            Console.WriteLine("Total TTC : \t\t" + Math.Round(totalTTC, 2) + " euros");
        }

        static void Main(string[] args)
        {
            int nbProduits;
            string nom, prenom, adresse, ville;
            int cp;

            string[] tabDes = new string[20];
            double[] tabPu = new double[20], tabMont = new double[20];
            int[] tabQ = new int[20];

            coordonnées(out nom, out prenom, out adresse, out ville, out cp);

            nbProduits = Saisie(ref tabDes, ref tabPu, ref tabQ, ref tabMont);

            affichage(nom, prenom, adresse, ville, cp, tabDes, tabPu, tabMont, tabQ, nbProduits);

            Console.ReadLine();
        }
    }
}
