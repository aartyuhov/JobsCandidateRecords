import React, {useEffect, useState} from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import {
    Avatar,
    Chip,
    CircularProgress,
    FormControl, IconButton,
    InputAdornment,
    InputLabel,
    OutlinedInput,
    TextField
} from '@mui/material';
import SidebarLink from '../../small-components/SidebarLink';
import {AccountCircle, SecurityOutlined, Visibility, VisibilityOff} from "@mui/icons-material";
import {Link} from "react-router-dom";

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
        </nav>
    </aside>
);

const SecurityForm = () => {
    const [showPassword, setShowPassword] = React.useState(false);
    const handleClickShowPassword = () => setShowPassword((show) => !show);
    const onSubmitHandler = (e) => {
        e.preventDefault();
    }

    return (
        <main className="flex-grow-1 p-4">
            <h1 className="fs-2 fw-semibold">Security</h1>
            <div className="mt-4 bg-white p-4 rounded shadow-sm">
                <h2 className="fs-4 fw-semibold">Password</h2>
                <form className="mt-4 w-50" onSubmit={onSubmitHandler}>
                    <div className="d-flex flex-lg-column flex-md-row mb-3">
                        <TextField
                            id="oldPassword"
                            name="oldPassword"
                            label="Old password"
                            variant="outlined"
                        />
                    </div>
                    <div className="d-flex flex-lg-column flex-md-row mb-3">
                        <FormControl variant="outlined">
                            <InputLabel htmlFor="newPassword">New password</InputLabel>
                            <OutlinedInput
                                id="newPassword"
                                name="newPassword"
                                type={showPassword ? 'text' : 'password'}
                                endAdornment={
                                    <InputAdornment position="end">
                                        <IconButton
                                            aria-label="toggle password visibility"
                                            onClick={handleClickShowPassword}
                                            edge="end"
                                        >
                                            {showPassword ? <VisibilityOff /> : <Visibility />}
                                        </IconButton>
                                    </InputAdornment>
                                }
                                label="New password"
                            />
                        </FormControl>
                    </div>
                    <div className="d-flex flex-lg-column flex-md-row mb-3">
                        <FormControl variant="outlined">
                            <InputLabel htmlFor="confirmNewPassword">Confirm new password</InputLabel>
                            <OutlinedInput
                                id="confirmNewPassword"
                                name="confirmNewPassword"
                                type={showPassword ? 'text' : 'password'}
                                endAdornment={
                                    <InputAdornment position="end">
                                        <IconButton
                                            aria-label="toggle password visibility"
                                            onClick={handleClickShowPassword}
                                            edge="end"
                                        >
                                            {showPassword ? <VisibilityOff /> : <Visibility />}
                                        </IconButton>
                                    </InputAdornment>
                                }
                                label="Confirm new password"
                            />
                        </FormControl>
                    </div>
                    <button type="submit" className="btn btn-primary mt-4 w-100">Reset new password</button>
                </form>
            </div>
        </main>
    );
};

const Security = () => {
    return (
        <div className="d-flex min-vh-100 bg-light">
            <AccountSidebar/>
            <SecurityForm/>
        </div>
    );
};

export default Security;
