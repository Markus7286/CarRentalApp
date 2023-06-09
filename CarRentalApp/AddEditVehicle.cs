﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class AddEditVehicle : Form
    {
        private bool isEditMode;
        private ManageVehicleListing _manageVehicleListing;
        private readonly CarRentalEntities _db;
        public AddEditVehicle(ManageVehicleListing manageVehicleListing = null)
        {
            InitializeComponent();
            lblTitle.Text = "Add New Vehicle";
            this.Text = "Add New Vehicle";
            isEditMode = false;
            _manageVehicleListing = manageVehicleListing;
            _db = new CarRentalEntities();
        }

        public AddEditVehicle(TypesOfCar carToEdit, ManageVehicleListing manageVehicleListing = null)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Vehicle";
            this.Text = "Edit Vehicle";
            _manageVehicleListing = manageVehicleListing;
            if (carToEdit == null) 
            {
                MessageBox.Show("Please ensure that you selected a valid record to edit");
                Close();
            }
            else
            {
                isEditMode = true;
                _db = new CarRentalEntities();
                PopulateFields(carToEdit);
            }
        }

        private void PopulateFields(TypesOfCar car)
        {
            lblId.Text = car.Id.ToString();
            tbMake.Text = car.Make;
            tbModel.Text = car.Model;
            tbVIN.Text = car.VIN;
            tbYear.Text = car.Year.ToString();
            tbLicenseNum.Text = car.LicensePlateNumber;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                try
                {
                    var id = int.Parse(lblId.Text);
                    var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);
                    car.Model = tbModel.Text;
                    car.Make = tbMake.Text;
                    car.VIN = tbVIN.Text;
                    car.Year = int.Parse(tbYear.Text);
                    car.LicensePlateNumber = tbLicenseNum.Text;

                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                try
                {
                    var newCar = new TypesOfCar
                    {
                        LicensePlateNumber = tbLicenseNum.Text,
                        Make = tbMake.Text,
                        Model = tbModel.Text,
                        VIN = tbVIN.Text,
                        Year = int.Parse(tbYear.Text)
                    };

                    _db.TypesOfCars.Add(newCar);
                    _db.SaveChanges();
                    _manageVehicleListing.PopulateGrid();
                    MessageBox.Show("Insert Operation Completed. Refresh Grid To See Changes");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void AddEditVehicle_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
