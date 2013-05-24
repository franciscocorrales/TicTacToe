using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace tictactoe
{
    public partial class TicTacToe : Form
    {
        bool esturnoJugadorX = true; //para saber si es el turno del jugador X, o de lo contrario del jugador O
        int cantidadTurnosJugados = 0;//cuando llega a 9 y no ha ganado, entonces ha sucedido un empate
        bool esEmpate = false;//dice si la partida terminó en empate


        public TicTacToe()
        {
            InitializeComponent();

        }

        private void TicTacToe_Load(object sender, EventArgs e)
        {
            labelTurnoJugadorO.Hide();//se escode O, ya que es el turno del jugador X, el primer movimiento.
        }



        //metodo principal, que realiza una jugada, recibe el botón que ha sido seleccionado y su indice del grid
        //uno de estos sería el indiceDelGrid:
        //    1 2 3
        //    4 5 6
        //    7 8 9
        private void hacerJugada(object sender, int indiceDelGrid) 
        {
            Button botonSeleccionado = sender as Button;

            //primero se verifica que el botón no haya sido escogido antes
            if (botonSeleccionado.Text.ToString() != "")
            {

            }
            else //si estaba sin escoger, entonces el jugador en turno lo selecciona
            {
                //la variable esturnoJugadorX nos va a decir de quién es el que está jugando el turno
                string jugadorEnTurno = "";
                if (esturnoJugadorX)
                    jugadorEnTurno = "X";
                else
                    jugadorEnTurno = "O";

                //se cambia el Text del botón
                botonSeleccionado.Text = jugadorEnTurno;
                botonSeleccionado.Refresh();
                System.Threading.Thread.Sleep(250);

                //se cambia la variable esturnoJugadorX, para así cambiar de turno
                if (esturnoJugadorX)
                {
                    esturnoJugadorX = false;
                    labelTurnoJugadorO.Visible = true;
                    labelTurnoJugadorX.Visible = false;

                }
                else
                {
                    esturnoJugadorX = true;
                    labelTurnoJugadorO.Visible = false;
                    labelTurnoJugadorX.Visible = true;
                }

                cantidadTurnosJugados++;//se aumenta el contador

                //ahora se verfica si se ganó o se debe seguir jugando
                if (verificarSiTerminoElJuego() && !esEmpate)
                {
                    //se aumentarán los contadores de victorias
                    if ( ! esturnoJugadorX)//hay que agregarle la negación, por que el valor ya lo cambiamos
                        labelContadorX.Text = Convert.ToString(Convert.ToInt16(labelContadorX.Text.ToString()) + 1);
                    else
                        labelContadorO.Text = Convert.ToString(Convert.ToInt16(labelContadorO.Text.ToString()) + 1);
                
                    //se resetean los botones y valores para el nuevo juego
                    
                    resetearValores();
                    
                }
                //si lo que sucedió fue un empate, no se suman los contadores de victoria
                if (esEmpate)
                {
                    //se resetean los botones y valores para el nuevo juego
                    
                    resetearValores();
                    labelContadorEmpates.Text = Convert.ToString(Convert.ToInt16(labelContadorEmpates.Text.ToString()) + 1);
                    
                }
                else
                {
                    //si no sucedio el empate, se puede seguir jugando,
                    //si la computadora debe jugar y es el turno del jugador O (computadora), realiza su movimiento
                    if (checkBoxContraComputadora.Checked == true && !esturnoJugadorX)
                    {
                        pensarJugadaComputadora(); //la computadora realiza su jugada
                        System.Threading.Thread.Sleep(250);
                    }
                }

            }
        }

        //la computadora hace su movimiento, en realidad no lo 'piensa'
        private void pensarJugadaComputadora() 
        {
            //    1 2 3
            //    4 5 6
            //    7 8 9

            //por diagonales
                  if (this.button1.Text.ToString().Equals(this.button5.Text.ToString()) &&
                      this.button9.Text.ToString() == "")
                           hacerJugada(this.button9, 9);
             else if (this.button5.Text.ToString().Equals(this.button9.Text.ToString()) &&
                      this.button1.Text.ToString() == "")
                           hacerJugada(this.button1, 1);
             else if (this.button3.Text.ToString().Equals(this.button5.Text.ToString()) &&
                      this.button7.Text.ToString() == "")
                           hacerJugada(this.button7, 7);
             else if (this.button5.Text.ToString().Equals(this.button7.Text.ToString()) &&
                      this.button3.Text.ToString() == "")
                           hacerJugada(this.button3, 3);
             else if (this.button1.Text.ToString().Equals(this.button9.Text.ToString()) &&
                      this.button5.Text.ToString() == "")
                           hacerJugada(this.button5, 5);
             else if (this.button7.Text.ToString().Equals(this.button3.Text.ToString()) &&
                      this.button5.Text.ToString() == "")
                           hacerJugada(this.button5, 5);
            //por filas
             else if (this.button1.Text.ToString().Equals(this.button2.Text.ToString()) &&
                      this.button3.Text.ToString() == "")
                           hacerJugada(this.button3, 3);
             else if (this.button2.Text.ToString().Equals(this.button3.Text.ToString()) &&
                      this.button1.Text.ToString() == "")
                           hacerJugada(this.button1, 1);
            else if (this.button4.Text.ToString().Equals(this.button5.Text.ToString()) &&
                     this.button6.Text.ToString() == "")
                          hacerJugada(this.button6, 6);
            else if (this.button5.Text.ToString().Equals(this.button6.Text.ToString()) &&
                     this.button4.Text.ToString() == "")
                          hacerJugada(this.button4, 4);
            else if (this.button7.Text.ToString().Equals(this.button8.Text.ToString()) &&
                     this.button9.Text.ToString() == "")
                          hacerJugada(this.button9, 9);
             else if (this.button8.Text.ToString().Equals(this.button9.Text.ToString()) &&
                      this.button7.Text.ToString() == "")
                            hacerJugada(this.button7, 7);
             else if (this.button1.Text.ToString().Equals(this.button3.Text.ToString()) &&
                      this.button2.Text.ToString() == "")
                           hacerJugada(this.button2, 2);
             else if (this.button4.Text.ToString().Equals(this.button6.Text.ToString()) &&
                      this.button5.Text.ToString() == "")
                           hacerJugada(this.button5, 5);
              else if (this.button7.Text.ToString().Equals(this.button9.Text.ToString()) &&
                           this.button8.Text.ToString() == "")
                      hacerJugada(this.button8, 8);
             //por columnas de momento no, para que se le pueda ganar a la computadora

            //buscar cualquiera:
           else    if(this.button5.Text.ToString() == "")
                   hacerJugada(this.button5, 5);
           else if(this.button7.Text.ToString() == "")
                   hacerJugada(this.button7, 7);
           else if(this.button4.Text.ToString() == "")
                   hacerJugada(this.button4, 4);
           else if(this.button3.Text.ToString() == "")
                   hacerJugada(this.button3, 3);
           else if(this.button1.Text.ToString() == "")
                   hacerJugada(this.button1, 1);
           else if(this.button6.Text.ToString() == "")
                   hacerJugada(this.button6, 6);
           else if(this.button2.Text.ToString() == "")
                   hacerJugada(this.button2, 2);
           else if(this.button8.Text.ToString() == "")
                   hacerJugada(this.button8, 8);
           else if(this.button9.Text.ToString() == "")
                   hacerJugada(this.button9, 9);
           
        }


        
        //verifica si se ha ganado la partida, o si ha terminado en empate
        private bool verificarSiTerminoElJuego()
        {
            //    1 2 3
            //    4 5 6
            //    7 8 9
            //se busca en  las filas si se ha ganado 
            if (this.button1.Text.ToString().Equals(this.button2.Text.ToString()) &&
                this.button2.Text.ToString().Equals(this.button3.Text.ToString()) &&
                this.button1.Text.ToString() != "")
               return true;
            if (this.button4.Text.ToString().Equals(this.button5.Text.ToString()) &&
                this.button5.Text.ToString().Equals(this.button6.Text.ToString()) &&
                this.button4.Text.ToString() != "")
                return true;
            if (this.button7.Text.ToString().Equals(this.button8.Text.ToString()) &&
                this.button8.Text.ToString().Equals(this.button9.Text.ToString()) &&
                this.button7.Text.ToString() != "")
                return true;

            //se busca en  las columnas si se ha ganado 
            if (this.button1.Text.ToString().Equals(this.button4.Text.ToString()) &&
                this.button4.Text.ToString().Equals(this.button7.Text.ToString()) &&
                this.button1.Text.ToString() != "")
                return true;
            if (this.button2.Text.ToString().Equals(this.button5.Text.ToString()) &&
                this.button5.Text.ToString().Equals(this.button8.Text.ToString()) &&
                this.button2.Text.ToString() != "")
                return true;
            if (this.button3.Text.ToString().Equals(this.button6.Text.ToString()) &&
                this.button6.Text.ToString().Equals(this.button9.Text.ToString()) &&
                this.button3.Text.ToString() != "")
                return true;

            //se busca en las diagonales si se ha ganado
            if (this.button1.Text.ToString().Equals(this.button5.Text.ToString()) &&
                this.button5.Text.ToString().Equals(this.button9.Text.ToString()) &&
                this.button1.Text.ToString() != "")
                return true;
            if (this.button3.Text.ToString().Equals(this.button5.Text.ToString()) &&
                this.button5.Text.ToString().Equals(this.button7.Text.ToString()) &&
                this.button3.Text.ToString() != "")
                return true;

            //se ve si se ha llegado a un empate, se actualiza la variable esEmpate
            if (cantidadTurnosJugados == 9)
                esEmpate = true;

            return false;
        }

        //resetea los valores, para que sirvan para el siguiente nuevo juego
        private void resetearValores() 
        {
            this.button1.Text = "";
            this.button2.Text = "";
            this.button3.Text = "";
            this.button4.Text = "";
            this.button5.Text = "";
            this.button6.Text = "";
            this.button7.Text = "";
            this.button8.Text = "";
            this.button9.Text = "";
            esturnoJugadorX = true;
            cantidadTurnosJugados = 0;
            labelTurnoJugadorO.Visible = false;
            labelTurnoJugadorX.Visible = true;
            esEmpate = false;
        }

       #region Botones

        private void button1_Click(object sender, EventArgs e)
        {
            hacerJugada(sender, 1);//se realiza la jugada, se envia el botón y el indice del botón en el grid
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hacerJugada(sender, 2);//se realiza la jugada, se envia el botón y el indice del botón en el grid
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hacerJugada(sender, 3);//se realiza la jugada, se envia el botón y el indice del botón en el grid
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hacerJugada(sender, 4);//se realiza la jugada, se envia el botón y el indice del botón en el grid
        }

        private void button5_Click(object sender, EventArgs e)
        {
            hacerJugada(sender, 5);//se realiza la jugada, se envia el botón y el indice del botón en el grid
        }

        private void button6_Click(object sender, EventArgs e)
        {
            hacerJugada(sender, 6);//se realiza la jugada, se envia el botón y el indice del botón en el grid
        }

        private void button7_Click(object sender, EventArgs e)
        {
            hacerJugada(sender, 7);//se realiza la jugada, se envia el botón y el indice del botón en el grid
        }

        private void button8_Click(object sender, EventArgs e)
        {
            hacerJugada(sender, 8);//se realiza la jugada, se envia el botón y el indice del botón en el grid
        }

        private void button9_Click(object sender, EventArgs e)
        {
            hacerJugada(sender, 9);//se realiza la jugada, se envia el botón y el indice del botón en el grid
        }

       #endregion


    }
}
