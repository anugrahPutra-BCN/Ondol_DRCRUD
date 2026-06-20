using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CRUDMahasiswaADO
{
    public partial class Dashboard : Form 
    {
        DAL dbLogic = new DAL(); //  Inisialisasi logika Data Access Layer (DAL) untuk interaksi database
 