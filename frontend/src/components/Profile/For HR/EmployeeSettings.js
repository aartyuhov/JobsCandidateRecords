import React, {useEffect, useState} from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import { TextField } from '@mui/material';
import {useParams} from "react-router-dom";

const EmployeeSettings = (props) => {

  let { id } = useParams();

  // useEffect(() => {
  //   console.log(`/something/${id}`);
  // },[id]);
  // const getdata = async (id) =>
  //   {
  //     try {
  //       const response = await axios.get(`/api/Employees/${id}`);
  //       if (!response.status) {
  //         throw new Error(`HTTP error! status: ${response.status}`);
  //       }
  //       setEmployee(response.data);
  //     } catch (error) {
  //       console.error(`Error fetching employee: ${error.message}`);
  //     }
  //   }
  // const [employee, setEmployee] = useState(() => null);

  return (
    <div className="d-flex align-items-center justify-content-center bg-light py-6 px-5">
      <div className="shadow-sm rounded-lg max-w-4xl w-100 d-flex flex-column flex-md-row">

        <div className="w-md-25 bg-primary text-white p-4 d-flex flex-column align-items-center">
          <img src="https://placehold.co/100x100" alt="profile-picture" className="rounded-circle mb-4" />
        </div>

        <div className="w-100 w-md-75 p-4">
          <h2 className="h4 font-weight-semibold mb-4">Profile Settings</h2>
          <form>
            <div className="d-flex flex-column flex-md-row mb-3 gap-4">
              <TextField required id="firstName" label="First name" variant="outlined"  defaultValue="none"/>
              <TextField required id="lastName" label="Last name" variant="outlined"  defaultValue="none"/>
            </div>
            <div className="d-flex flex-column flex-md-row mb-3">
              <div className="w-100 w-md-50 me-md-3">
                <TextField required id="phoneNumber" label="Phone number" variant="outlined"  defaultValue="none"/>
              </div>
              <div className="w-100 w-md-50">
                <TextField required id="email" label="Email" variant="outlined"  defaultValue="none"/>
              </div>
            </div>
            <div className="d-flex flex-column flex-md-row mb-3">
              <div className="w-100 w-md-50 me-md-3">
                <TextField required id="position" label="Position" variant="outlined"  defaultValue="none"/>
              </div>
              <div className="w-100 w-md-50">
                <TextField required id="gender" label="Gender" variant="outlined"  defaultValue="none"/>
              </div>
            </div>
            <div className="d-flex flex-column flex-md-row mb-3">
              <div className="w-100 w-md-50 me-md-3">
                <div className="form-floating mb-3">
                  <input type="text" className="form-control" id="hireDate" placeholder="Hire Date"  />
                  <label htmlFor="hireDate">Hire Date</label>
                </div>
              </div>
              <div className="w-100 w-md-50">
                <div className="form-floating mb-3">
                  <input type="text" className="form-control" id="dateOfBirth" placeholder="Date Of Birth" />
                  <label htmlFor="dateOfBirth">Date Of Birth</label>
                </div>
              </div>
            </div>
            <div className="mb-3">
              <div className="form-floating mb-3">
                <input type="text" className="form-control" id="addressLine" placeholder="Enter address"  />
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
