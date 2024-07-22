import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import { TextField } from '@mui/material';

const EmployeeSettings = () => {

  
  const getdata = async (id) =>
    {
      try {
        const response = await axios.get(`/api/Employees/${id}`);
        if (!response.status) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        setEmployee(response.data);
      } catch (error) {
        console.error(`Error fetching employee: ${error.message}`);
      }
    }
  const [employee, setEmployee] = useState(() => getdata(1));

  return (
    <div className="d-flex align-items-center justify-content-center bg-light py-6 px-5">
      <div className="shadow-sm rounded-lg max-w-4xl w-100 d-flex flex-column flex-md-row">

        <div className="w-md-25 bg-primary text-white p-4 d-flex flex-column align-items-center">
          <img src="https://placehold.co/100x100" alt="profile-picture" className="rounded-circle mb-4" />
          <h2 className="h5 font-weight-semibold">{employee.firstName} {employee.lastName}</h2>
          <p className="small">{employee.email}</p>
        </div>

        <div className="w-100 w-md-75 p-4">
          <h2 className="h4 font-weight-semibold mb-4">Profile Settings</h2>
          <form>
            <div className="d-flex flex-column flex-md-row mb-3 gap-4">
              <TextField required id="firstName" label="First name" variant="outlined" value={employee.firstName} defaultValue="none"/>
              <TextField required id="lastName" label="Last name" variant="outlined" value={employee.lastName} defaultValue="none"/>
            </div>
            <div className="d-flex flex-column flex-md-row mb-3">
              <div className="w-100 w-md-50 me-md-3">
                <div className="form-floating mb-3">
                <input type="text" className="form-control" id="phoneNumber" placeholder="Enter phone number" value={employee.phoneNumber}/>
                <label htmlFor="phoneNumber">Phone Number</label>
                </div>
              </div>
              <div className="w-100 w-md-50">
                <div className="form-floating mb-3">
                  <input type="email" className="form-control" id="email" placeholder="Enter email" value={employee.email}/>
                  <label htmlFor="email">Email</label>
                </div>
              </div>
            </div>
            <div className="d-flex flex-column flex-md-row mb-3">
              <div className="w-100 w-md-50 me-md-3">
                <div className="form-floating mb-3">
                  <input type="text" className="form-control" id="position" placeholder="Position" value='Possss'/>
                  <label htmlFor="position">Position</label>
                </div>
              </div>
              <div className="w-100 w-md-50">
                <div className="form-floating mb-3">
                  <input type="text" className="form-control" id="gender" placeholder="Gender" value={employee.gender}/>
                  <label htmlFor="gender">Gender</label>
                </div>
              </div>
            </div>
            <div className="d-flex flex-column flex-md-row mb-3">
              <div className="w-100 w-md-50 me-md-3">
                <div className="form-floating mb-3">
                  <input type="text" className="form-control" id="hireDate" placeholder="Hire Date" value={employee.hireDate} />
                  <label htmlFor="hireDate">Hire Date</label>
                </div>
              </div>
              <div className="w-100 w-md-50">
                <div className="form-floating mb-3">
                  <input type="text" className="form-control" id="dateOfBirth" placeholder="Date Of Birth" value={employee.dateOfBirth}/>
                  <label htmlFor="dateOfBirth">Date Of Birth</label>
                </div>
              </div>
            </div>
            <div className="mb-3">
              <div className="form-floating mb-3">
                <input type="text" className="form-control" id="addressLine" placeholder="Enter address" value={employee.address} />
                <label htmlFor="addressLine">Address</label>
              </div>
            </div>
            <button type="submit" className="btn btn-primary mt-4">Save Profile</button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default EmployeeSettings;
