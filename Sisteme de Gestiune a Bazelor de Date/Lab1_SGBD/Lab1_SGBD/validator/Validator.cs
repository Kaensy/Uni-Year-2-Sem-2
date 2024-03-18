using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_SGBD.validator
{
    public class Validator
    {
        /* Validate the inputs for a new film */
        public bool ValidateInputs(string name, DateTime startDate, DateTime endDate, string location, float prizePool, int game, int Organizer)
        {
            // Validate title
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter a name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate release date
            if (startDate == DateTime.MinValue)
            {
                MessageBox.Show("Please select a valid release date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate release date
            if (endDate == DateTime.MinValue)
            {
                MessageBox.Show("Please select a valid release date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate description
            if (string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show("Please enter a description.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate content rating
            if (string.IsNullOrWhiteSpace(game))
            {
                MessageBox.Show("Please enter a content rating.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            // Validate budget
            if (prizePool < 0)
            {
                MessageBox.Show("Please enter a valid budget greater than or equal to 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate box office
            if (string.IsNullOrWhiteSpace(Organizer))
            {
                MessageBox.Show("Please enter a valid box office value greater than or equal to 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // All inputs are valid
            return true;
        }
    }
}
