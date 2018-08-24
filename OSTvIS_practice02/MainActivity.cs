using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace OSTvIS_practice02
{
    [Activity(Label = "OSTvIS_practice02", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int fi = 0, n = 0, d, eps, S;
        int[] C, M, M2, H;
        List<string> listString = new List<string>();
        List<int> listInt = new List<int>();
        ArrayAdapter<string> adapterString;
        ArrayAdapter<int> adapterInt;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button1 = FindViewById<Button>(Resource.Id.button1);
            EditText p = FindViewById<EditText>(Resource.Id.editText1);
            EditText q = FindViewById<EditText>(Resource.Id.editText2);
            TextView fiAndN = FindViewById<TextView>(Resource.Id.textView5);
            Spinner spinnerD = FindViewById<Spinner>(Resource.Id.spinner1);

            Button button2 = FindViewById<Button>(Resource.Id.button2);
            EditText editD = FindViewById<EditText>(Resource.Id.editText3);
            Spinner spinnerE = FindViewById<Spinner>(Resource.Id.spinner2);

            Button button3 = FindViewById<Button>(Resource.Id.button3);
            EditText editE = FindViewById<EditText>(Resource.Id.editText4);
            EditText editMarray1 = FindViewById<EditText>(Resource.Id.editText5);
            TextView MarrayAndCarray = FindViewById<TextView>(Resource.Id.textView10);

            Button button4 = FindViewById<Button>(Resource.Id.button4);
            EditText editMarray2 = FindViewById<EditText>(Resource.Id.editText6);
            EditText editHnull = FindViewById<EditText>(Resource.Id.editText7);
            TextView Harray = FindViewById<TextView>(Resource.Id.textView13);

            TextView messageBox = FindViewById<TextView>(Resource.Id.textView6);

            button1.Click += delegate
            {
                try
                {
                    fi = (Convert.ToInt32(p.Text) - 1) * (Convert.ToInt32(q.Text) - 1);
                    n = Convert.ToInt32(p.Text) * Convert.ToInt32(q.Text);
                    fiAndN.Text = "f(n) = " + fi + "\r\nn = " + n;

                    bool check;
                    listInt.Clear();
                    for (int i = 2; i < fi; i++)
                    {
                        check = true;
                        for (int k = 2; k <= i; k++)
                        {
                            if (((fi % k) == 0) && ((i % k) == 0))
                            {
                                check = false;
                                break;
                            }
                        }
                        if (check)
                        {
                            listInt.Add(i);
                        }
                    }

                    adapterInt = new ArrayAdapter<int>(this, Android.Resource.Layout.SimpleSpinnerItem, listInt);
                    adapterInt.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    spinnerD.Adapter = adapterInt;
                }
                catch (Exception ex)
                {
                    messageBox.Text = ex.Message + "\r\n" + ex.StackTrace;
                }
            };

            spinnerD.ItemSelected += delegate
            {
                editD.Text = spinnerD.SelectedItem.ToString();
            };

            button2.Click += delegate
            {
                try
                {
                    d = Convert.ToInt32(editD.Text);
                    listString.Clear();
                    for (int i = (((fi * d) - 1) / fi); i > 0; i--)
                    {
                        listString.Add(Convert.ToString((double)((fi * i) + 1) / d) + " (k = " + i + ")");
                    }
                    adapterString = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, listString);
                    adapterString.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    spinnerE.Adapter = adapterString;
                }
                catch (Exception ex)
                {
                    messageBox.Text = ex.Message + "\r\n" + ex.StackTrace;
                }
            };

            spinnerE.ItemSelected += delegate
            {
                editE.Text = spinnerE.SelectedItem.ToString().Substring(0, spinnerE.SelectedItem.ToString().IndexOf("(") - 1);
            };

            button3.Click += delegate
            {
                try
                {
                    eps = Convert.ToInt32(editE.Text);
                    string alfavit = "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя";
                    string text = editMarray1.Text;
                    MarrayAndCarray.Text = text + " = (";
                    M = new int[text.Length];
                    for (int i = 0; i < M.Length; i++)
                    {
                        M[i] = alfavit.IndexOf(text.Substring(i, 1)) / 2 + 1;
                        MarrayAndCarray.Text += M[i] + ", ";
                    }
                    MarrayAndCarray.Text = MarrayAndCarray.Text.Substring(0, (MarrayAndCarray.Text.Length - 2)) + ")\r\n";

                    C = new int[text.Length];
                    for (int i = 0; i < C.Length; i++)
                    {
                        C[i] = mod(M[i], eps, n);
                        MarrayAndCarray.Text += "C[" + i + "] = (" + M[i] + "^" + eps + ") mod " + n + " = " + C[i] + "\r\n";
                    }

                    M2 = new int[text.Length];
                    for (int i = 0; i < M2.Length; i++)
                    {
                        M2[i] = mod(C[i], d, n);
                        MarrayAndCarray.Text += "M2[" + i + "] = (" + C[i] + "^" + d + ") mod " + n + " = " + M2[i] + "\r\n";
                    }
                }
                catch (Exception ex)
                {
                    messageBox.Text = ex.Message + "\r\n" + ex.StackTrace;
                }
            };

            button4.Click += delegate
            {
                try
                {
                    string alfavit = "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя";
                    string text = editMarray2.Text;
                    Harray.Text = text + " = (";
                    M = new int[text.Length];
                    for (int i = 0; i < M.Length; i++)
                    {
                        M[i] = alfavit.IndexOf(text.Substring(i, 1)) / 2 + 1;
                        Harray.Text += M[i] + ", ";
                    }
                    Harray.Text = Harray.Text.Substring(0, (Harray.Text.Length - 2)) + ")\r\n";

                    H = new int[text.Length + 1];
                    H[0] = Convert.ToInt32(editHnull.Text);
                    for (int i = 1; i < H.Length; i++)
                    {
                        H[i] = mod(H[i - 1] + M[i - 1], 2, n);
                        Harray.Text += "H[" + i + "] = ((" + H[i - 1] + " + " + M[i - 1] + ")^2) mod " + n + " = " + H[i] + "\r\n";
                    }

                    S = mod(H[H.Length - 1], d, n);
                    Harray.Text += "\r\nS = (" + H[H.Length - 1] + "^" + d + ") mod " + n + " = " + S + "\r\n";
                    Harray.Text += "H = (" + S + "^" + eps + ") mod " + n + " = " + mod(S, eps, n) + "\r\n";
                }
                catch (Exception ex)
                {
                    messageBox.Text = ex.Message + "\r\n" + ex.StackTrace;
                }
            };
        }

        private int mod(int delimoe, int stepen, int delitel)
        {
            int[] arr = new int[Int16.MaxValue];
            // = (delimoe^stepen) mod delitel
            arr[0] = (delimoe % delitel);
            for (int k = 1; ; k++)
            {
                arr[k] = (delimoe * arr[k - 1]) % delitel;
                if (arr[k] == arr[0])
                {
                    return arr[(stepen % k) - 1];
                }
            }
        }
    }
}


