using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace JogoMemoria
{
    [Activity(Label = "AtividadeFase2", Theme = "@style/NoStatusBar", MainLauncher = false)]
    public class AtividadeFase2 : Activity
    {
        int posicaoFruta1, posicaoFruta2;
        bool travaClique = false;
        int contadorAcertos, contadorErros = 0;
        string fruta1, fruta2 = "";
        int cartaCount = 0;
        int count = 0;
        Timer timer, timer2 = null;
        Random rnd;
        int[] posicoes;
        int[] verificador;
        string[] fruits = new string[16];
        int posicao;
        ImageButton _img1f2, _img2f2, _img3f2, _img4f2, _img5f2, _img6f2, _img7f2, _img8f2, _img9f2, _img10f2, _img11f2, _img12f2, _img13f2, _img14f2, _img15f2, _img16f2;
        TextView _txtAcertosf2, _txtErrosf2, _txtObjetivof2;
        Button _btnJogarf2, _btnMenuf2;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.tela_fase_2);

            _img1f2 = FindViewById<ImageButton>(Resource.Id.img1f2);
            _img2f2 = FindViewById<ImageButton>(Resource.Id.img2f2);
            _img3f2 = FindViewById<ImageButton>(Resource.Id.img3f2);
            _img4f2 = FindViewById<ImageButton>(Resource.Id.img4f2);
            _img5f2 = FindViewById<ImageButton>(Resource.Id.img5f2);
            _img6f2 = FindViewById<ImageButton>(Resource.Id.img6f2);
            _img7f2 = FindViewById<ImageButton>(Resource.Id.img7f2);
            _img8f2 = FindViewById<ImageButton>(Resource.Id.img8f2);
            _img9f2 = FindViewById<ImageButton>(Resource.Id.img9f2);
            _img10f2 = FindViewById<ImageButton>(Resource.Id.img10f2);
            _img11f2 = FindViewById<ImageButton>(Resource.Id.img11f2);
            _img12f2 = FindViewById<ImageButton>(Resource.Id.img12f2);
            _img13f2 = FindViewById<ImageButton>(Resource.Id.img13f2);
            _img14f2 = FindViewById<ImageButton>(Resource.Id.img14f2);
            _img15f2 = FindViewById<ImageButton>(Resource.Id.img15f2);
            _img16f2 = FindViewById<ImageButton>(Resource.Id.img16f2);
            _txtAcertosf2 = FindViewById<TextView>(Resource.Id.txtAcertosf2);
            _txtErrosf2 = FindViewById<TextView>(Resource.Id.txtErrosf2);
            _txtObjetivof2 = FindViewById<TextView>(Resource.Id.txtObjetivof2);
            _btnJogarf2 = FindViewById<Button>(Resource.Id.btnJogarf2);
            _btnMenuf2 = FindViewById<Button>(Resource.Id.btnMenuf2);
            // Create your application here

            _img1f2.Click += _img1f2_Click;
            _img2f2.Click += _img2f2_Click;
            _img3f2.Click += _img3f2_Click;
            _img4f2.Click += _img4f2_Click;
            _img5f2.Click += _img5f2_Click;
            _img6f2.Click += _img6f2_Click;
            _img7f2.Click += _img7f2_Click;
            _img8f2.Click += _img8f2_Click;
            _img9f2.Click += _img9f2_Click;
            _img10f2.Click += _img10f2_Click;
            _img11f2.Click += _img11f2_Click;
            _img12f2.Click += _img12f2_Click;
            _img13f2.Click += _img13f2_Click;
            _img14f2.Click += _img14f2_Click;
            _img15f2.Click += _img15f2_Click;
            _img16f2.Click += _img16f2_Click;
            _btnJogarf2.Click += _btnJogarf2_Click;
            _btnMenuf2.Click += _btnMenuf2_Click;
            startGame();
            _txtObjetivof2.SetTextColor(Android.Graphics.Color.Green);
        }

        private void _btnMenuf2_Click(object sender, EventArgs e)
        {
            backToHome();
        }

        private void _btnJogarf2_Click(object sender, EventArgs e)
        {
            restartGame();
        }

        public void backToHome()
        {
            Intent it = new Intent(this, typeof(AtividadeTelaPrincipal));
            StartActivity(it);
        }
        public void restartGame()
        {
            contadorErros = 0;
            contadorAcertos = 0;
            setDefaultStyle();
            startGame();
        }
        public void endGame(string title, string msg)
        {
            _btnJogarf2.Enabled = true;
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alerta = builder.Create();
            alerta.SetCancelable(true);
            alerta.SetIcon(Android.Resource.Drawable.IcDialogInfo);
            alerta.SetTitle(title);
            alerta.SetMessage(msg);

            if (contadorAcertos == 8 && contadorErros <= 3)
            {
                alerta.SetButton("Próxima fase", (s, ev) =>
                {
                    Intent intent = new Intent(this, typeof(AtividadeFase3));
                    StartActivity(intent);
                });
                alerta.SetButton2("Jogar novamente", (s, ev) =>
                {
                    restartGame();
                });
                
            }
            else if (contadorErros > 3)
            {
                alerta.SetButton2("Jogar novamente", (s, ev) =>
                {
                    restartGame();
                });
            }

            alerta.Show();
        }
        public void setFruits(int p)
        {
            if (travaClique) { return; }
            ImageButton[] img = getBtn();
            if (fruits[p] == "pear")
            {
                img[p].SetBackgroundResource(Resource.Drawable.pera_cartoon);
                img[p].Enabled = false;
            }
            else if (fruits[p] == "apple")
            {
                img[p].SetBackgroundResource(Resource.Drawable.maca_cartoon);
                img[p].Enabled = false;
            }
            else if (fruits[p] == "orange")
            {
                img[p].SetBackgroundResource(Resource.Drawable.laranja_cartoon);
                img[p].Enabled = false;
            }
            else if (fruits[p] == "watermelon")
            {
                img[p].SetBackgroundResource(Resource.Drawable.melancia_cartoon);
                img[p].Enabled = false;
            }
            else if (fruits[p] == "mango")
            {
                img[p].SetBackgroundResource(Resource.Drawable.manga_cartoon);
                img[p].Enabled = false;
            }
            else if (fruits[p] == "melon")
            {
                img[p].SetBackgroundResource(Resource.Drawable.melao_cartoon);
                img[p].Enabled = false;
            }
            else if (fruits[p] == "grape")
            {
                img[p].SetBackgroundResource(Resource.Drawable.uva_cartoon);
                img[p].Enabled = false;
            }
            else
            {
                img[p].SetBackgroundResource(Resource.Drawable.banana_cartoon);
                img[p].Enabled = false;
            }
            cartaCount++;
            if (cartaCount == 1)
            {
                fruta1 = fruits[p];
                posicaoFruta1 = p;
            }
            else if (cartaCount == 2)
            {
                fruta2 = fruits[p];
                posicaoFruta2 = p;
                cartaCount = 0;
                if (fruta1 == fruta2)
                {
                    contadorAcertos++;
                    _txtAcertosf2.Text = contadorAcertos.ToString();
                }
                else
                {
                    contadorErros++;
                    _txtErrosf2.Text = contadorErros.ToString();
                    travaClique = true;
                    img[posicaoFruta1].Enabled = true;
                    img[posicaoFruta2].Enabled = true;
                    temporizador2();
                }
                if (contadorAcertos == 8 && contadorErros <= 3)
                {
                    endGame("Parabéns, você ganhou!", "Deseja ir para próxima fase?");
                    _txtAcertosf2.SetTextColor(Android.Graphics.Color.Green);
                }else if (contadorErros > 3)
                {
                    endGame("Ops, você perdeu!", "Tente novamente!");
                    _txtErrosf2.SetTextColor(Android.Graphics.Color.Red);
                }
            }
        }
        public void temporizador2()
        {
            timer2 = new Timer();
            timer2.Interval = 1000;
            timer2.Elapsed += Timer_Elapsed1;
            timer2.Start();
        }

        private void Timer_Elapsed1(object sender, ElapsedEventArgs e)
        {
            RunOnUiThread(() => {

                //Toast.MakeText(this, "ok" + posicao + " ", ToastLength.Short).Show();
                ImageButton[] img = getBtn();
                img[posicaoFruta1].SetBackgroundResource(Resource.Drawable.bordaRedonda);
                img[posicaoFruta2].SetBackgroundResource(Resource.Drawable.bordaRedonda);
                travaClique = false;
                timer2.Stop();

            });
        }


        public void Temporizador()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            count++;
            RunOnUiThread(() => {
                if (count > 2)
                {
                    count = 0;
                    setDefaultStyle();
                    timer.Stop();

                }
            });
        }
        public ImageButton[] getBtn()
        {
            ImageButton[] camposBtn =
            {
                _img1f2,
                _img2f2, 
                _img3f2, 
                _img4f2, 
                _img5f2,
                _img6f2, 
                _img7f2, 
                _img8f2,
                _img9f2,
                _img10f2, 
                _img11f2, 
                _img12f2, 
                _img13f2, 
                _img14f2, 
                _img15f2, 
                _img16f2

        };

            return camposBtn;
        }

        public void setDefaultStyle()
        {
            ImageButton[] btnImgCampos = getBtn();
            for (int i = 0; i < btnImgCampos.Length; i++)
            {
                btnImgCampos[i].SetBackgroundResource(Resource.Drawable.bordaRedonda);
                btnImgCampos[i].Enabled = true;
            }
        }
        public bool existe(int[] posicoes, int posicao, int tamanho)
        {
            bool valor = false;
            for (int i = 0; i < tamanho; i++)
            {
                if (posicoes[i] == posicao)
                {
                    valor = true;
                }
            }
            return valor;
        }
        public void startGame()
        {
            _btnJogarf2.Enabled = false;
            posicoes = new int[16];
            verificador = new int[16];

            rnd = new Random();
            //int tamanho = 0;
            for (int i = 0; i < posicoes.Length; i++)
            {

                posicao = rnd.Next(1, 17);
                while (existe(posicoes, posicao, i))
                {
                    posicao = rnd.Next(1, 17);
                }
                posicoes[i] = posicao;


                ImageButton[] btn = getBtn();
                if (posicoes[i] == 1)
                {
                    fruits[i] = "pear";
                    btn[i].SetBackgroundResource(Resource.Drawable.pera_cartoon);

                }
                else if (posicoes[i] == 2)
                {
                    fruits[i] = "apple";
                    btn[i].SetBackgroundResource(Resource.Drawable.maca_cartoon);
                }
                else if (posicoes[i] == 3)
                {
                    fruits[i] = "orange";
                    btn[i].SetBackgroundResource(Resource.Drawable.laranja_cartoon);
                }
                else if (posicoes[i] == 4)
                {
                    fruits[i] = "watermelon";
                    btn[i].SetBackgroundResource(Resource.Drawable.melancia_cartoon);
                }
                else if (posicoes[i] == 5)
                {
                    fruits[i] = "pear";
                    btn[i].SetBackgroundResource(Resource.Drawable.pera_cartoon);
                }
                else if (posicoes[i] == 6)
                {
                    fruits[i] = "apple";
                    btn[i].SetBackgroundResource(Resource.Drawable.maca_cartoon);
                }
                else if (posicoes[i] == 7)
                {
                    fruits[i] = "orange";
                    btn[i].SetBackgroundResource(Resource.Drawable.laranja_cartoon);
                }
                else if (posicoes[i] == 8)
                {
                    fruits[i] = "watermelon";
                    btn[i].SetBackgroundResource(Resource.Drawable.melancia_cartoon);
                }
                else if (posicoes[i] == 9)
                {
                    fruits[i] = "mango";
                    btn[i].SetBackgroundResource(Resource.Drawable.manga_cartoon);
                }
                else if (posicoes[i] == 10)
                {
                    fruits[i] = "melon";
                    btn[i].SetBackgroundResource(Resource.Drawable.melao_cartoon);
                }
                else if (posicoes[i] == 11)
                {
                    fruits[i] = "grape";
                    btn[i].SetBackgroundResource(Resource.Drawable.uva_cartoon);
                }
                else if (posicoes[i] == 12)
                {
                    fruits[i] = "banana";
                    btn[i].SetBackgroundResource(Resource.Drawable.banana_cartoon);
                }
                else if (posicoes[i] == 13)
                {
                    fruits[i] = "mango";
                    btn[i].SetBackgroundResource(Resource.Drawable.manga_cartoon);
                }
                else if (posicoes[i] == 14)
                {
                    fruits[i] = "melon";
                    btn[i].SetBackgroundResource(Resource.Drawable.melao_cartoon);
                }
                else if (posicoes[i] == 15)
                {
                    fruits[i] = "grape";
                    btn[i].SetBackgroundResource(Resource.Drawable.uva_cartoon);
                }
                else
                {
                    fruits[i] = "banana";
                    btn[i].SetBackgroundResource(Resource.Drawable.banana_cartoon);
                }
              
            }
            Temporizador();
        }

        private void _img16f2_Click(object sender, EventArgs e)
        {
            setFruits(15);
        }

        private void _img15f2_Click(object sender, EventArgs e)
        {
            setFruits(14);
        }

        private void _img14f2_Click(object sender, EventArgs e)
        {
            setFruits(13);
        }

        private void _img13f2_Click(object sender, EventArgs e)
        {
            setFruits(12);
        }

        private void _img12f2_Click(object sender, EventArgs e)
        {
            setFruits(11);
        }

        private void _img11f2_Click(object sender, EventArgs e)
        {
            setFruits(10);
        }

        private void _img10f2_Click(object sender, EventArgs e)
        {
            setFruits(9);
        }

        private void _img9f2_Click(object sender, EventArgs e)
        {
            setFruits(8);
        }

        private void _img8f2_Click(object sender, EventArgs e)
        {
            setFruits(7);
        }

        private void _img7f2_Click(object sender, EventArgs e)
        {
            setFruits(6);
        }

        private void _img6f2_Click(object sender, EventArgs e)
        {
            setFruits(5);
        }

        private void _img5f2_Click(object sender, EventArgs e)
        {
            setFruits(4);
        }

        private void _img4f2_Click(object sender, EventArgs e)
        {
            setFruits(3);
        }

        private void _img3f2_Click(object sender, EventArgs e)
        {
            setFruits(2);
        }

        private void _img2f2_Click(object sender, EventArgs e)
        {
            setFruits(1);
        }

        private void _img1f2_Click(object sender, EventArgs e)
        {
            setFruits(0);
        }
    }
}