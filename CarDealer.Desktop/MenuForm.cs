using System;
using System.Windows.Forms;
using CarDealer.Data;
using Model;

namespace CarDealer.Desktop
{
    public partial class Menuform : Form
    {
        // Repository
        private readonly CarDealerRepository _carRepository;

        // Control references - Details Tab
        private TextBox txtVIN;
        private ComboBox cboMake;
        private TextBox txtModel;
        private NumericUpDown numYear;
        private NumericUpDown numMileage;
        private TextBox txtColor;
        private TextBox txtDescription;

        // Control references - Specs Tab
        private ComboBox cboFuel;
        private ComboBox cboTransmission;
        private NumericUpDown numEngineSize;
        private NumericUpDown numPowerHP;
        private NumericUpDown numDoors;
        private NumericUpDown numSeats;

        // Control references - Pricing Tab
        private NumericUpDown numPrice;
        private DateTimePicker dtpFirstReg;
        private DateTimePicker dtpPurchaseDate;
        private TextBox txtLocation;

        // Buttons
        private Button btnSave;
        private Button btnClear;

        public Menuform()
        {
            InitializeComponent();
            _carRepository = new CarDealerRepository();
            InitializeCarDetailsUI();
        }

        private void InitializeCarDetailsUI()
        {
            this.Text = "Car Details - Vehicle Manager";
            this.Size = new System.Drawing.Size(1000, 700);

            tabDetails.Text = "Vehicle Details";
            tabSpecs.Text = "Specifications";
            tabPricing.Text = "Pricing & Dates";
            tabMedia.Text = "Photos";

            BuildDetailsTab();
            BuildSpecsTab();
            BuildPricingTab();
            BuildMediaTab();
        }

        private void BuildDetailsTab()
        {
            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                Padding = new Padding(20),
                AutoScroll = true,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            // VIN
            txtVIN = new TextBox { MaxLength = 17, Width = 300 };
            AddLabeledControl(tlp, 0, "VIN:", txtVIN);

            // Make (Brand)
            cboMake = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDown,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems,
                Width = 300
            };
            cboMake.Items.AddRange(new object[] { "Audi", "BMW", "Mercedes-Benz", "Volkswagen", "Ford", "Toyota", "Honda" });
            AddLabeledControl(tlp, 1, "Make:", cboMake);

            // Model
            txtModel = new TextBox { Width = 300 };
            AddLabeledControl(tlp, 2, "Model:", txtModel);

            // Year
            numYear = new NumericUpDown { Minimum = 1980, Maximum = 2100, Value = DateTime.Now.Year, Width = 150 };
            AddLabeledControl(tlp, 3, "Year:", numYear);

            // Mileage
            numMileage = new NumericUpDown { Maximum = 999999, Increment = 1000, Width = 150 };
            AddLabeledControl(tlp, 4, "Mileage (km):", numMileage);

            // Color
            txtColor = new TextBox { Width = 200 };
            AddLabeledControl(tlp, 5, "Color:", txtColor);

            // Description
            txtDescription = new TextBox { Multiline = true, Height = 80, Width = 400, ScrollBars = ScrollBars.Vertical };
            AddLabeledControl(tlp, 6, "Description:", txtDescription);

            // Buttons
            var pnlButtons = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                Padding = new Padding(0, 10, 0, 10)
            };

            btnSave = new Button { Text = "Save", Width = 100, Height = 30 };
            btnSave.Click += BtnSave_Click; // WIRE UP EVENT            
            btnClear = new Button { Text = "Clear", Width = 100, Height = 30, Margin = new Padding(10, 0, 0, 0) };
            btnClear.Click += BtnClear_Click; // WIRE UP EVENT 
            pnlButtons.Controls.AddRange(new Control[] { btnSave, btnClear });

            tlp.Controls.Add(new Label(), 0, 7); // spacer
            tlp.Controls.Add(pnlButtons, 1, 7);

            tabDetails.Controls.Clear();
            tabDetails.Controls.Add(tlp);
        }

        private void BuildSpecsTab()
        {
            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                Padding = new Padding(20),
                AutoScroll = true
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            // Fuel Type
            cboFuel = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 200 };
            cboFuel.Items.AddRange(new object[] { "Petrol", "Diesel", "Hybrid", "Electric", "LPG" });
            AddLabeledControl(tlp, 0, "Fuel Type:", cboFuel);

            // Transmission
            cboTransmission = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 200 };
            cboTransmission.Items.AddRange(new object[] { "Manual", "Automatic", "CVT", "Semi-Automatic" });
            AddLabeledControl(tlp, 1, "Transmission:", cboTransmission);

            // Engine Size (cc)
            numEngineSize = new NumericUpDown { Maximum = 10000, Increment = 100, Width = 150 };
            AddLabeledControl(tlp, 2, "Engine Size (cc):", numEngineSize);

            // Power
            numPowerHP = new NumericUpDown { Maximum = 2000, Increment = 10, Width = 150 };
            AddLabeledControl(tlp, 3, "Power (HP):", numPowerHP);

            // Doors
            numDoors = new NumericUpDown { Minimum = 2, Maximum = 6, Value = 4, Width = 100 };
            AddLabeledControl(tlp, 4, "Doors:", numDoors);

            // Seats
            numSeats = new NumericUpDown { Minimum = 2, Maximum = 9, Value = 5, Width = 100 };
            AddLabeledControl(tlp, 5, "Seats:", numSeats);

            tabSpecs.Controls.Add(tlp);
        }

        private void BuildPricingTab()
        {
            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                Padding = new Padding(20),
                AutoScroll = true
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            // Price
            numPrice = new NumericUpDown
            {
                DecimalPlaces = 2,
                Maximum = 10000000,
                Increment = 1000,
                Width = 200,
                ThousandsSeparator = true
            };
            AddLabeledControl(tlp, 0, "Price (€):", numPrice);

            // First Registration
            dtpFirstReg = new DateTimePicker { Width = 200, ShowCheckBox = true };
            AddLabeledControl(tlp, 1, "First Registration:", dtpFirstReg);

            // Purchase Date
            dtpPurchaseDate = new DateTimePicker { Width = 200, ShowCheckBox = true };
            AddLabeledControl(tlp, 2, "Purchase Date:", dtpPurchaseDate);

            // Location
            txtLocation = new TextBox { Width = 300 };
            AddLabeledControl(tlp, 3, "Location:", txtLocation);

            tabPricing.Controls.Add(tlp);
        }

        private void BuildMediaTab()
        {
            var mainPanel = new Panel { Dock = DockStyle.Fill };

            // Top panel - buttons
            var pnlTop = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(20, 15, 20, 15),
                BackColor = System.Drawing.Color.WhiteSmoke
            };
            var btnAddPhotos = new Button { Text = "Add Photos", Width = 120, Height = 30 };
            var btnRemove = new Button { Text = "Remove Selected", Width = 120, Height = 30, Margin = new Padding(10, 0, 0, 0) };
            pnlTop.Controls.AddRange(new Control[] { btnAddPhotos, btnRemove });

            // Photo panel
            var pnlPhotos = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20),
                BackColor = System.Drawing.Color.White
            };

            var lblPlaceholder = new Label
            {
                Text = "No photos added yet.\nClick 'Add Photos' to upload vehicle images.",
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10F),
                ForeColor = System.Drawing.Color.Gray,
                Padding = new Padding(20)
            };
            pnlPhotos.Controls.Add(lblPlaceholder);

            mainPanel.Controls.Add(pnlPhotos);
            mainPanel.Controls.Add(pnlTop);
            pnlTop.BringToFront();

            tabMedia.Controls.Add(mainPanel);
        }

        // SAVE BUTTON CLICK HANDLER
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(cboMake.Text))
                {
                    MessageBox.Show("Make (Brand) is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabMain.SelectedTab = tabDetails;
                    cboMake.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtModel.Text))
                {
                    MessageBox.Show("Model is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabMain.SelectedTab = tabDetails;
                    txtModel.Focus();
                    return;
                }

                // Create Car object from form data
                var car = new car.Car
                {
                    Brand = cboMake.Text,
                    Model = txtModel.Text,
                    Year = (int)numYear.Value,
                    Price = numPrice.Value,
                    Currency = "EUR",

                    // Optional fields
                    Vin = string.IsNullOrWhiteSpace(txtVIN.Text) ? null : txtVIN.Text,
                    Mileage = numMileage.Value == 0 ? null : (int)numMileage.Value,
                    Color = string.IsNullOrWhiteSpace(txtColor.Text) ? null : txtColor.Text,
                    Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text,

                    // Specs
                    FuelType = cboFuel.SelectedItem?.ToString(),
                    Transmission = cboTransmission.SelectedItem?.ToString(),
                    EngineSizeCc = numEngineSize.Value == 0 ? null : (int)numEngineSize.Value,
                    PowerHp = numPowerHP.Value == 0 ? null : (int)numPowerHP.Value,
                    Doors = numDoors.Value == 0 ? null : (byte)numDoors.Value,
                    Seats = numSeats.Value == 0 ? null : (byte)numSeats.Value,

                    // Pricing/Dates
                    FirstRegistration = dtpFirstReg.Checked ? dtpFirstReg.Value : null,
                    PurchaseDate = dtpPurchaseDate.Checked ? dtpPurchaseDate.Value : null
                };

                // Save to database
                int newCarId = _carRepository.AddCar(car);

                MessageBox.Show($"Car saved successfully!\n\nCar ID: {newCarId}\nBrand: {car.Brand}\nModel: {car.Model}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear form after successful save
                ClearForm();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show($"Database Error:\n\n{sqlEx.Message}\n\nError Number: {sqlEx.Number}\nState: {sqlEx.State}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving car:\n\n{ex.Message}\n\nType: {ex.GetType().Name}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // CLEAR BUTTON CLICK HANDLER
        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear all fields?", "Confirm Clear",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearForm();
            }
        }

        private void ClearForm()
        {
            // Details
            txtVIN.Clear();
            cboMake.SelectedIndex = -1;
            cboMake.Text = "";
            txtModel.Clear();
            numYear.Value = DateTime.Now.Year;
            numMileage.Value = 0;
            txtColor.Clear();
            txtDescription.Clear();

            // Specs
            cboFuel.SelectedIndex = -1;
            cboTransmission.SelectedIndex = -1;
            numEngineSize.Value = 0;
            numPowerHP.Value = 0;
            numDoors.Value = 4;
            numSeats.Value = 5;

            // Pricing
            numPrice.Value = 0;
            dtpFirstReg.Checked = false;
            dtpPurchaseDate.Checked = false;
            txtLocation.Clear();

            // Focus back to first field
            tabMain.SelectedTab = tabDetails;
            cboMake.Focus();
        }

        private void AddLabeledControl(TableLayoutPanel tlp, int row, string labelText, Control control)
        {
            while (tlp.RowStyles.Count <= row)
                tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var lbl = new Label
            {
                Text = labelText,
                AutoSize = true,
                Anchor = AnchorStyles.Right,
                Padding = new Padding(0, 6, 10, 6)
            };

            control.Anchor = AnchorStyles.Left;
            control.Margin = new Padding(0, 3, 0, 3);

            tlp.Controls.Add(lbl, 0, row);
            tlp.Controls.Add(control, 1, row);
        }

        private void Menuform_Load(object sender, EventArgs e)
        {
            // Form load event - can be used for loading data
        }

        private void tabPricing_Click(object sender, EventArgs e)
        {
            // Tab click event
        }

        private void tabDetails_Click(object sender, EventArgs e)
        {

        }
    }
}
