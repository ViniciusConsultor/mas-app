using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using VisitaJayaPerkasa.SqlRepository;
using VisitaJayaPerkasa.Entities;

namespace VisitaJayaPerkasa.Form.Report.Request_Price
{
    public partial class rptSupplyPriceControl : UserControl
    {
        public rptSupplyPriceControl()
        {
            InitializeComponent();
            SqlCityRepository sqlCityRepository = new SqlCityRepository();
            SqlTypeContRepository sqlTypeContRepository = new SqlTypeContRepository();


            List<TypeCont> listTypeCont = sqlTypeContRepository.GetTypeCont();
            List<City> listCity = sqlCityRepository.GetCity();

            cboTypeCont.DataSource = listTypeCont;
            cboTypeCont.DisplayMember = "TypeName";
            cboTypeCont.Value = "ID";

            cboDestination.DataSource = listCity;
            cboDestination.DisplayMember = "CityName";
            cboDestination.Value = "ID";

            cboDestination.SelectedIndex = -1;
            cboDestination.Text = Constant.VisitaJayaPerkasaApplication.cboDefaultText;
            cboTypeCont.SelectedIndex = -1;
            cboTypeCont.Text = Constant.VisitaJayaPerkasaApplication.cboDefaultText;

            sqlCityRepository = null;
            sqlTypeContRepository = null;
            listTypeCont = null;
            listCity = null;
        }

        private void radButtonElementBtnSearch_Click(object sender, EventArgs e)
        {
            if (cboDestination.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
            {
                MessageBox.Show(this, "Please choose destination", "Information");
                return;
            }
            else if (cboTypeCont.Text.Equals(Constant.VisitaJayaPerkasaApplication.cboDefaultText))
            {
                MessageBox.Show(this, "Please choose type cont", "Information");
                return;
            }
            else if (etPrice.Text.Trim().Equals(""))
            {
                MessageBox.Show(this, "Please fill price", "Information");
                return;
            }
            else if (etOffice.Text.Trim().Equals(""))
            {
                MessageBox.Show(this, "Please fill office", "Information");
                return;
            }
            try
            {
                Int32.Parse(etPrice.Text.Trim());
            }
            catch {
                MessageBox.Show(this, "Price must be numeric", "Information");
                return;
            }



            ParameterFields pluralParameter = new ParameterFields();
            ParameterField singleParameter;
            ParameterDiscreteValue parameterDiscreteValue;


            singleParameter = new ParameterField();
            singleParameter.Name = "Destination";
            parameterDiscreteValue = new ParameterDiscreteValue();
            parameterDiscreteValue.Value = cboDestination.Text;
            singleParameter.CurrentValues.Add(parameterDiscreteValue);
            pluralParameter.Add(singleParameter);


            singleParameter = new ParameterField();
            singleParameter.Name = "Price";
            parameterDiscreteValue = new ParameterDiscreteValue();
            parameterDiscreteValue.Value = etPrice.Text.Trim();
            singleParameter.CurrentValues.Add(parameterDiscreteValue);
            pluralParameter.Add(singleParameter);


            singleParameter = new ParameterField();
            singleParameter.Name = "TypeCont";
            parameterDiscreteValue = new ParameterDiscreteValue();
            parameterDiscreteValue.Value = cboTypeCont.Text;
            singleParameter.CurrentValues.Add(parameterDiscreteValue);
            pluralParameter.Add(singleParameter);


            singleParameter = new ParameterField();
            singleParameter.Name = "Office";
            parameterDiscreteValue = new ParameterDiscreteValue();
            parameterDiscreteValue.Value = etOffice.Text;
            singleParameter.CurrentValues.Add(parameterDiscreteValue);
            pluralParameter.Add(singleParameter);

            crystalReportViewer1.ParameterFieldInfo = pluralParameter;
            RequestPrice.rptSupplyPrice objRptSupplyPrice = new RequestPrice.rptSupplyPrice();
            crystalReportViewer1.ReportSource = objRptSupplyPrice;

            singleParameter = null;
            parameterDiscreteValue = null;
            pluralParameter = null;
            objRptSupplyPrice = null;
        }

        private void cboDestination_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }

        private void cboTypeCont_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(0);
        }
    }
}
