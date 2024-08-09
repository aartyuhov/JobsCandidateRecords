import React, {useEffect, useState} from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import {Avatar, Chip, CircularProgress, TextField} from '@mui/material';
import {AccountCircle, SecurityOutlined} from "@mui/icons-material";
import {Link} from "react-router-dom";
import api from "../../../services/api";

const AccountSidebar = () => (
  <aside className="w-25 bg-white p-4 border-end">
    <nav>
      <h2 className="fs-6 text-secondary">Personal</h2>
      <ul className="list-group list-group-flush mt-2">
        <li className="list-group-item p-2">
          <Link to="/account"
                className="d-flex align-items-center text-decoration-none text-dark p-2 rounded no-underline">
            <AccountCircle sx={{width: 24, height: 24}}/> <span className='px-3'>Account</span>
          </Link>
        </li>
        <li className="list-group-item p-2">
          <Link to="/security"
                className="d-flex align-items-center text-decoration-none text-dark p-2 rounded no-underline">
            <SecurityOutlined sx={{width: 24, height: 24}}/> <span className='px-3'>Security</span>
          </Link>
        </li>
      </ul>
      {/* <h2 className="fs-6 text-secondary mt-4">Organization</h2>
      <ul className="list-group list-group-flush mt-2">
        <ProfileLink iconSrc="https://placehold.co/24x24?text=💳" text="Billing & plans" />
        <ProfileLink iconSrc="https://placehold.co/24x24?text=👥" text="Team" />
        <ProfileLink iconSrc="https://placehold.co/24x24?text=🔗" text="Integrations" />
      </ul> */}
    </nav>
  </aside>
);

const AccountForm = ( { data } ) => {
  const [firstName, setFirstName] = useState(data.firstName);
  const [lastName, setLastName] = useState(data.lastName);
  const [phoneNumber, setPhoneNumber] = useState(data.phoneNumber);
  const [email, setEmail] = useState(data.email);
  const [address, setAddress] = useState(data.address);

  return (
    <main className="flex-grow-1 p-4">
      <h1 className="fs-2 fw-semibold">
       Account
      </h1>
      <div className="mt-4 bg-white p-4 rounded shadow-sm">
        <h2 className="fs-4 fw-semibold">Basic details</h2>
        <form className="mt-4">
          <div className="d-flex align-items-center mb-lg-2">
            <Avatar
                sx={{width: 80, height: 80}}
                src={data.avatarUrl ?? ''}
            />
            <button type="button" className="btn btn-outline-danger ms-3">Remove</button>
            <div className={"mx-lg-3 d-flex gap-2"}>
              <Chip className={"fs-6"} label={data.positionName ?? "Asp .Net Developer"} variant="outlined"/>
              <Chip className={"fs-6 text-bg-info"} label={data.gender ?? "None gender"} variant="outlined"/>
              <Chip className={"fs-6 text-dark-50"} style={{background: "#89b2d9"}}
                    label={"Date of birth " + new Date(data.dateOfBirth).toLocaleDateString() ?? "None date"}
                    variant="outlined"/>
            </div>
          </div>
          <p className={"mb-lg-5 text-secondary"}>Has been working
            since {new Date(data.hireDate).toLocaleDateString()}</p>
          <div className="d-flex flex-column flex-md-row mb-3 gap-4">
            <TextField
                required
                id="firstName"
                name="firstName"
                label="First name"
                variant="outlined"
                value={firstName}
                onChange={e => setFirstName(e.target.value)}
            />
            <TextField
                required
                id="lastName"
                name="lastName"
                label="Last name"
                variant="outlined"
                value={lastName}
                onChange={e => setLastName(e.target.value)}
            />
          </div>
          <div className="d-flex flex-lg-column flex-md-row mb-3">
            <TextField
                id="phoneNumber"
                name="phoneNumber"
                label="Phone number"
                variant="outlined"
                value={phoneNumber}
                onChange={e => setPhoneNumber(e.target.value)}
            />
          </div>
          <div className="d-flex flex-lg-column flex-md-row mb-3">
            <TextField
                id="email"
                name="email"
                label="Email"
                variant="outlined"
                value={email}
                onChange={e => setEmail(e.target.value)}
            />
          </div>
          <div className="d-flex flex-lg-column flex-md-row mb-2">
            <TextField
                id="address"
                name="address"
                label="Address"
                variant="outlined"
                value={address}
                onChange={e => setAddress(e.target.value)}
            />
          </div>
          <button type="submit" className="btn btn-primary mt-4">Save changes</button>
        </form>
      </div>
    </main>
  );
};

const Account = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await api.get(`/api/Users/get-tuple`);
        setData(response.data.employeeDto);
        setLoading(false);
      } catch (error) {
        console.error('Error fetching data:', error);
        setError('An error occurred while fetching the data. Please try again later.');
        setLoading(false);
      }
    };

    fetchData();
  }, []);
  return (
    <div className="d-flex min-vh-100 bg-light">
      <AccountSidebar />
      { loading ? (
         <div className={"d-flex align-content-center"}>
           <CircularProgress color="inherit" />
         </div>
        ) : error ? (
            <h4 className={"text-center"}>{error}</h4>
        ) :
        (
          <AccountForm data={data} />
        )
      }
    </div>
  );
};

export default Account;