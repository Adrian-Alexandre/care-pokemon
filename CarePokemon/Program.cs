using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Drawing;

namespace CarePokemon
{
    internal class Program
    {
        //inicio da imagem
        static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            // Cria um novo Bitmap com a largura e altura desejadas
            Bitmap resizedImage = new Bitmap(width, height);

            // Desenha a imagem original no novo Bitmap usando as dimensões desejadas
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }

            return resizedImage;
        }

        static string ConvertToAscii(Bitmap image)
        {
            // Caracteres ASCII usados para representar a imagem
            char[] asciiChars = { ' ', '.', ':', '-', '=', '+', '*', '#', '%', '@' };

            StringBuilder asciiArt = new StringBuilder();

            // Percorre os pixels da imagem e converte cada um em um caractere ASCII correspondente
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int grayScale = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    int asciiIndex = grayScale * (asciiChars.Length - 1) / 255;
                    char asciiChar = asciiChars[asciiIndex];
                    asciiArt.Append(asciiChar);
                }
                asciiArt.Append(Environment.NewLine);
            }

            return asciiArt.ToString();
        }

        static void ExibirImagem(string imagePath, int width, int height)
        {
            // Caminho para a imagem que deseja exibir
            //string imagePath = @"C:\Users\Danilo Filitto\Downloads\Panda.jpg";

            // Carrega a imagem
            Bitmap image = new Bitmap(imagePath);

            // Redimensiona a imagem para a largura e altura desejadas
            int consoleWidth = width;
            int consoleHeight = height;
            Bitmap resizedImage = ResizeImage(image, consoleWidth, consoleHeight);

            // Converte a imagem em texto ASCII
            string asciiArt = ConvertToAscii(resizedImage);

            // Exibe o texto ASCII no console
            Console.WriteLine(asciiArt);


        }
        //fim da imagem
        static void SalvarDados(ref string nome, ref string nomePlayer,ref string pokemon, ref float alimentado, ref float limpo, ref float feliz)
        {
            string dir = Environment.CurrentDirectory + "\\";
            string file = dir + nome + nomePlayer + ".txt";
            if (File.Exists(file))
            {
                string [] dados = File.ReadAllLines(file);
                alimentado = float.Parse(dados[2]);
                limpo = float.Parse(dados[3]);
                feliz = float.Parse(dados[4]);
            }
            if (alimentado <= 0 || limpo <= 0 || feliz <= 0)
            {
                alimentado = 100;
                limpo = 100;
                feliz = 100;
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("Seu pokemon ficou muito fraco!!");
                Console.WriteLine("Levamos ele no centro pokemon para se curar!!!");
                Console.WriteLine("Agora ele está saudável e feliz!!!");
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("Aperte <qualquer tecla> para continuar.");
                Console.WriteLine("-----------------------------------------------");

            }
        }

        static void ArmazenarDados(string nome, string nomePlayer, float alimentado, float limpo, float feliz)
        {
            string dir = Environment.CurrentDirectory + "\\";
            string file = dir + nome + nomePlayer + ".txt";
            string fileContent = nome + Environment.NewLine;
            fileContent += nomePlayer + Environment.NewLine;
            fileContent += alimentado + Environment.NewLine;
            fileContent += limpo + Environment.NewLine;
            fileContent += feliz + Environment.NewLine;
            File.WriteAllText(file, fileContent);
        }

        static string Falas()
        {
            //falas do pokemon
            Random rand = new Random();
            string[] frases = new string[5];
            frases[0] = "A vida de um pokemon não é facil viu...";
            frases[1] = "Treinei pra caramba hoje!!!";
            frases[2] = "Bora dominar esse mundo pokemon?";
            frases[3] = "Tomara que eu evolua logo.";
            frases[4] = "Você é um ótimo treinador.";
            return frases[rand.Next(frases.Length)];
        }

        static void AttCaract(ref float alimentado, ref float limpo, ref float feliz)
        {
            Console.Clear();
            //alterar o status do Pokemon
            // 0 - alimento; 1 - Limpo; 2 - Feliz
            Random rand = new Random();
            int caract = 0;
            caract = rand.Next(3);
            switch (caract)
            {
                case 0: alimentado -= rand.Next(18); break;
                case 1: limpo -= rand.Next(18); break;
                case 2: feliz -= rand.Next(18); break;
            }

        }

        static void EstadoPoke(ref float alimentado, ref float limpo, ref float feliz)
        {
            if (alimentado > 20 && alimentado < 50)
            {
                Console.WriteLine("To morrendo de fome!!!");
                Console.WriteLine("Me de alguma coisa pra comer por favor!!!");
            }
            else if (limpo > 20 && limpo < 50)
            {
                Console.WriteLine("Que cheirinho estranho...");
                Console.WriteLine("Acho que to precisando de um banho!!!");
            }
            else if (feliz > 20 && feliz < 50)
            {
                Console.WriteLine("Estou ficando muito triste, tomara que não entre em depressão.");
                Console.WriteLine("Bora brincar de alguma coisa?");
            }
            else
            {
                Console.WriteLine(Falas());
                Thread.Sleep(2600);
                Console.Clear();
                Console.WriteLine(Falas());
                Thread.Sleep(2600);
                Console.Clear();
            }
        }

        static void PergPoke(ref string entrada, ref float alimentado, ref float limpo, ref float feliz)
        {
            Random rand = new Random();
            Console.WriteLine("O que vamos fazer hoje?");
            Console.Write("Comer | Banho| Brincar | Nada:");
            entrada = Console.ReadLine().ToLower();
            switch (entrada)
            {
                case "brincar": feliz += rand.Next(20); break;
                case "comer": alimentado += rand.Next(20); break;
                case "banho": limpo += rand.Next(20); break;
            }
            if (feliz > 100) feliz = 100;
            if (alimentado > 100) alimentado = 100;
            if (limpo > 100) limpo = 100;

            if (entrada == "brincar")
            {
                Console.WriteLine("Para brincar com o seu pokemon aperte <qualquer tecla> para sair aperte <ESC>");
            }
            else if (entrada == "comer")
            {
                Console.WriteLine("Para alimentar o seu pokemon aperte <qualquer tecla> para sair aperte <ESC>");
            }
            else if (entrada == "banho")
            {
                Console.WriteLine("Para dar banho no seu pokemon aperte <qualquer tecla> para sair aperte <ESC>");
            }
            else if (entrada == "nada")
            {
                Console.WriteLine("Para continuar aperte <qualquer tecla> para sair aperte <ESC>");
            }
            else if (entrada == "")
            {
                Console.WriteLine("Para continuar aperte <qualquer tecla> para sair aperte <ESC>");
            }
        }

        static void ResultProg(ref float alimentado, ref float limpo, ref float feliz)
        {
            if (alimentado <= 0 || limpo <= 0 || feliz <= 0)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("!!!      GAME OVER     !!!");
                Console.WriteLine("!!! Seu pokemon MORREU !!!");
                Console.WriteLine("--------------------------");
                Console.WriteLine("Da próxima cuide melhor dele...");
                Console.WriteLine("aperte <qualquer tecla> para fechar o jogo");
            }
            else
            {
                Console.WriteLine("Obrigado por cuidar do seu Pokemon!!!");
                Console.WriteLine("Até a próxima!!!");
                Console.WriteLine("aperte <qualquer tecla> para fechar o jogo");
            }
        }

        static void PokeStatus(ref float alimentado, ref float limpo, ref float feliz)
        {
            Console.WriteLine("Você gostaria de ver os status do seu pokemon? (S/N)");
            Console.Write("Digite:");
            string verStatus = Console.ReadLine().ToLower();
            if (verStatus == "s")
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("  STATUS DO POKEMON  ");
                Console.WriteLine("---------------------");
                Console.WriteLine("Fome: {0}", alimentado);
                Console.WriteLine("Higiene: {0}", limpo);
                Console.WriteLine("Felicidade: {0}", feliz);
                Console.WriteLine("---------------------");
                Thread.Sleep(3000);
                Console.Clear();
            }
            else
            {
                Console.Clear();
            }

        }

        static void InputDados(string[] args, ref string nome, ref string nomePlayer,ref string pokemon)
        {
 
            //Entrada de dados - Nomes
            if (args.Length > 0)
            {
                nome = args[0];
            }
            else
            {
            Console.WriteLine("Qual é o nome do seu Pokemon?");
            Console.Write("Digite:");
            nome = Console.ReadLine();
            if(nome == "")
            {
                nome += pokemon;
            }
            Console.WriteLine("Oi, qual é o nome do meu dono?");
            Console.Write("Digite:");
            nomePlayer = Console.ReadLine();
            Console.WriteLine("Legal, estava com muita saudade de você {0}", nomePlayer);
            if(nomePlayer == "")
            {
                nomePlayer = "User";
            }
            }
        }

        static void EscolhaPokemon(ref string pokemon, ref string charmander, ref string bulbassauro, ref string squirtle, ref string pikachu)
        {

            do
            {
                Console.WriteLine("Para visualizar melhor o seu pokemon, use o programa em tela cheia!!!");
                Console.WriteLine("De qual Pokemon você gostaria de cuidar?");
                Console.Write("Charmander, Bulbassauro, Squirtle, Pikachu:");
                pokemon = Console.ReadLine().ToLower();
                
                switch (pokemon)
                {
                    case "charmander":
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("PARABÉNS VOCÊ ESCOLHEU O CHARMANDER!!!");
                        Console.WriteLine("---------------------------------------");
                        ExibirImagem(charmander, 60, 60); break;
                    case "bulbassauro":
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("PARABÉNS VOCÊ ESCOLHEU O BULBASSAURO!!!");
                        Console.WriteLine("---------------------------------------");
                        ExibirImagem(bulbassauro, 60, 60); break;
                    case "squirtle":
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("PARABÉNS VOCÊ ESCOLHEU O SQUIRTLE!!!");
                        Console.WriteLine("------------------------------------");
                        ExibirImagem(squirtle, 60, 60); break;
                    case "pikachu":
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("PARABÉNS VOCÊ ESCOLHEU O PIKACHU!!!");
                        Console.WriteLine("-----------------------------------");
                        ExibirImagem(pikachu, 60, 60); break;
                    case "": Console.WriteLine("VOCÊ PRECISA ESCOLHER UM POKEMON!!!"); break;
                }
            } while (pokemon == "");

        }

        static void PossuiPoke(ref string possuiPoke, ref string[] args, ref string nome, ref string nomePlayer, ref float alimentado, ref float limpo, ref float feliz, ref string pokemon, ref string charmander, ref string bulbassauro, ref string squirtle, ref string pikachu)
        {
            do
            {
                Console.WriteLine("Você já possui um pokemon pra cuidar?");
                Console.Write("Digite (S/N): ");
                possuiPoke = Console.ReadLine().ToLower();
                switch (possuiPoke)
                {
                    case "s":
                        //Entrada de dados - Nomes
                        InputDados(args, ref nome, ref nomePlayer, ref pokemon);
                        //salvar os dados do Pokemon no arquivo texto
                        SalvarDados(ref nome, ref nomePlayer, ref pokemon, ref alimentado, ref limpo, ref feliz);
                        Console.ReadKey();
                        break;
                    case "n":
                        //Escolhe qual pokemon o usuario quer cuidar
                        EscolhaPokemon(ref pokemon, ref charmander, ref bulbassauro, ref squirtle, ref pikachu);
                        //Entrada de dados - Nomes
                        InputDados(args, ref nome, ref nomePlayer, ref pokemon);
                        break;
                    case "": 
                        Console.WriteLine("Digite (S) ou (N):");
                        break;
                }
            } while (possuiPoke == "");
        }

        static void Main(string[] args)
        {
            /*
              O jogo tem como objetivo que o usuário cuide de um pokemon dentro do console.
              O game mantém os dados do pokemon mesmo ao fechar.
              Inicia o programa via prompt e coleta o nome.
              Coleta os dados de alimento, limpo e feliz dentro de um arquivo texto.
              De tempos em tempos os respectivos valores das características do pokemon serão dominuídos.
              Sempre que o usuário voltar a jogar, pode voltar a cuidar do seu respectivo pokemon.
            */

            string nome = null; //nome dado pelo jogador ao pokemon
            string nomePlayer = null; //nome do jogador
            string entrada = ""; //entrada de dados
            string pokemon = ""; //nome original do pokemon
            string possuiPoke = ""; //Verifica se o usuário já possui um pókemon
            
            //status do pokemon
            float alimentado = 100;
            float limpo = 100;
            float feliz = 100;

            //arquivo das imagens dos pokemons
            string charmander = Environment.CurrentDirectory + "\\charmander.jpg";
            string bulbassauro = Environment.CurrentDirectory + "\\bulbassauro.jpg";
            string squirtle = Environment.CurrentDirectory + "\\squirtle.jpg";
            string pikachu = Environment.CurrentDirectory + "\\pikachu.jpg";

            //título do game
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("CUIDE DO SEU POKEMON by Adrian Alexandre");
            Console.WriteLine("----------------------------------------");
            
            //Verifica se o usuário ja possui um pokemon
            PossuiPoke(ref possuiPoke, ref args, ref nome, ref nomePlayer, ref alimentado, ref limpo, ref feliz, ref pokemon, ref charmander, ref bulbassauro, ref squirtle, ref pikachu);


            do
            {
                //Decrementa as Características do Pokemon
                AttCaract(ref alimentado, ref limpo, ref feliz);

                //Estado do Pokemon
                EstadoPoke(ref alimentado, ref limpo, ref feliz);

                //Ver Status do Pokemon
                PokeStatus(ref alimentado, ref limpo, ref feliz);

                //Interação com usuário
                PergPoke(ref entrada, ref alimentado, ref limpo, ref feliz);

            } while (Console.ReadKey().Key != ConsoleKey.Escape && alimentado > 0 && feliz > 0 && limpo > 0);

            //Resultado do programa
            ResultProg(ref alimentado, ref limpo, ref feliz);

            //armazenar os dados
            ArmazenarDados(nome, nomePlayer, alimentado, limpo, feliz);

        }
    }
}
