import React, { useEffect, useState } from 'react';
import {
    Paper,
    Avatar,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    TableSortLabel,
    IconButton,
    Card,
    CircularProgress,
    Stack,
    Divider,
    Typography,
    Modal,
    Box,
    TextField,
    Button,
    FormControl,
    InputLabel,
    OutlinedInput,
    InputAdornment,
    Chip,
    MenuItem,
    Select,
    ListItemText
} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import AddIcon from '@mui/icons-material/Add';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import CloseIcon from '@mui/icons-material/Close'; // Add CloseIcon for removing role
import 'bootstrap/dist/css/bootstrap.min.css';
import SearchBar from "../small-components/SearchBar";
import { useNavigate } from "react-router-dom";
import api from "../../services/api";

const UserList = () => {
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [users, setUsers] = useState([]);
    const [roles, setRoles] = useState([]);
    const [employees, setEmployees] = useState([]);
    const [search, setSearch] = useState('');
    const [sortBy, setSortBy] = useState('');
    const [sortOrder, setSortOrder] = useState('asc');
    const [open, setOpen] = useState(false);
    const [roleOpen, setRoleOpen] = useState(false);
    const [selectedRole, setSelectedRole] = useState('');
    const [selectedUser, setSelectedUser] = useState(null);
    const [username, setUsername] = useState('');
    const [userEmail, setUserEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [showPassword, setShowPassword] = useState(false);
    const [errorText, setErrorText] = useState('');
    const [isEditing, setIsEditing] = useState(false);
    const [selectedEmployee, setSelectedEmployee] = useState('');

    useEffect(() => {
        const fetchData = async () => {
            try {
                const userResponse = await api.get(`/api/Users`);
                const usersData = userResponse.data;

                const usersWithRoles = await Promise.all(usersData.map(async (user) => {
                    const rolesResponse = await api.get(`/api/Users/roles/${user.id}`);
                    return { ...user, roles: rolesResponse.data };
                }));
                setUsers(usersWithRoles);

                const employeeResponse = await api.get(`/api/Employees`);
                setEmployees(employeeResponse.data);

                const rolesResponse = await api.get(`/api/Roles`);
                setRoles(rolesResponse.data);

                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
                setError('An error occurred while fetching the data. Please try again later.');
                setLoading(false);
            }
        };

        fetchData();
    }, []);

    const handleSearchChange = (event) => {
        setSearch(event.target.value);
    };

    const handleSort = (property) => {
        const isAsc = sortBy === property && sortOrder === 'asc';
        setSortOrder(isAsc ? 'desc' : 'asc');
        setSortBy(property);
    };

    const handleOpen = (user = null) => {
        if (user) {
            setSelectedUser(user);
            setUsername(user.userName);
            setUserEmail(user.email);
            setSelectedEmployee(user.employee || '');
            setIsEditing(true);
        } else {
            setSelectedUser(null);
            setUsername('');
            setUserEmail('');
            setPassword('');
            setConfirmPassword('');
            setSelectedEmployee('');
            setIsEditing(false);
        }
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
        setErrorText('');
    };

    const handleSubmit = async () => {
        if (password !== confirmPassword) {
            setErrorText("Passwords do not match.");
            return;
        }

        const userData = {
            username,
            userName: username,
            email: userEmail,
            password,
            oldPassword: password,
            newPassword: password,
            confirmPassword: password
        };

        try {
            if (isEditing) {
                await api.put(`/api/Users/${selectedUser.id}`, userData);
                setUsers(users.map(user =>
                    user.id === selectedUser.id ? { ...user, userName: username, email: userEmail, employee: selectedEmployee, roles:[] } : user
                ));
            } else {
                const createdUserResponse = await api.post(`/api/Users`, userData);
                const createdUserId = createdUserResponse.data;
                if (selectedEmployee) {
                    await api.post(`/api/Users/${createdUserId}/assign/${selectedEmployee}`);
                }
                setUsers([...users, { ...userData, employee: selectedEmployee, roles:[] }]);
                window.location.reload();
            }
            setOpen(false);
        } catch (error) {
            setErrorText("An error occurred while saving the user.");
        }
    };

    const handleClickShowPassword = () => setShowPassword(!showPassword);

    const handleRemoveRole = async (userId, role) => {
        try {
            await api.delete(`/api/Roles/${userId}/remove/${role}`);
            setUsers(users.map(user =>
                user.id === userId
                    ? { ...user, roles: user.roles.filter(r => r !== role) }
                    : user
            ));
        } catch (error) {
            setErrorText('Failed to remove the role.');
        }
    };

    const filteredUsers = users
        .filter(user =>
            user.userName.toLowerCase().includes(search.toLowerCase()) ||
            user.email.toLowerCase().includes(search.toLowerCase())
        )
        .sort((a, b) => {
            if (sortBy === '') return 0;
            if (sortOrder === 'asc') {
                return a[sortBy] > b[sortBy] ? 1 : -1;
            } else {
                return a[sortBy] < b[sortBy] ? 1 : -1;
            }
        });

    const handleOpenRoleModal = (user) => {
        setSelectedUser(user);
        setRoleOpen(true);
    };

    const handleRoleClose = () => {
        setRoleOpen(false);
        setSelectedRole('');
    };

    const handleRoleSubmit = async () => {
        if (!selectedRole) {
            setErrorText('Please select a role.');
            return;
        }

        try {
            await api.post(`/api/Roles/${selectedUser.id}/assign/${selectedRole}`);
            setUsers(users.map(user =>
                user.id === selectedUser.id
                    ? { ...user, roles: [...user.roles, selectedRole] }
                    : user
            ));
            setRoleOpen(false);
        } catch (error) {
            setErrorText('Failed to change the role.');
        }
        setSelectedRole('');
    };

    return (
        <Card sx={{ width: '85%', marginTop: 4, marginX: "auto" }}>
            <Paper className="p-3 mb-3 d-flex justify-content-center">
                <SearchBar handleSearchChange={handleSearchChange} search={search} />
                <IconButton color="primary" onClick={() => handleOpen(null)}>
                    <AddIcon />
                </IconButton>
            </Paper>

            {loading ? (
                <div className="d-flex align-content-center">
                    <CircularProgress color="inherit" />
                </div>
            ) : error ? (
                <h4 className="text-center">{error}</h4>
            ) : (
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell></TableCell>
                                <TableCell>
                                    <TableSortLabel
                                        active={sortBy === 'userName'}
                                        direction={sortOrder}
                                        onClick={() => handleSort('userName')}
                                    >
                                        Username
                                    </TableSortLabel>
                                </TableCell>
                                <TableCell>
                                    <TableSortLabel
                                        active={sortBy === 'email'}
                                        direction={sortOrder}
                                        onClick={() => handleSort('email')}
                                    >
                                        Email
                                    </TableSortLabel>
                                </TableCell>
                                <TableCell>Roles</TableCell>
                                <TableCell>Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {filteredUsers.map(user => (
                                <TableRow key={user.id}>
                                    <TableCell>
                                        <Avatar>{user.userName[0]}</Avatar>
                                    </TableCell>
                                    <TableCell>{user.userName}</TableCell>
                                    <TableCell>{user.email}</TableCell>
                                    <TableCell>
                                        {user.roles.map(role => (
                                            <Chip
                                                key={role}
                                                label={role}
                                                onDelete={() => handleRemoveRole(user.id, role)}
                                                deleteIcon={<CloseIcon />} // Add CloseIcon for removing role
                                                sx={{ margin: '2px' }}
                                            />
                                        ))}
                                    </TableCell>
                                    <TableCell>
                                        <IconButton onClick={() => handleOpen(user)}>
                                            <EditIcon />
                                        </IconButton>
                                        <IconButton onClick={() => handleOpenRoleModal(user)}>
                                            <Visibility />
                                        </IconButton>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}

            <Modal open={open} onClose={handleClose}>
                <Box sx={{ ...style, width: 400 }}>
                    <Typography variant="h6" component="h2">
                        {isEditing ? 'Edit User' : 'Create User'}
                    </Typography>
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="username"
                        label="Username"
                        name="username"
                        autoComplete="username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                    />
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="email"
                        label="Email Address"
                        name="email"
                        autoComplete="email"
                        value={userEmail}
                        onChange={(e) => setUserEmail(e.target.value)}
                    />
                    {!isEditing ? (
                        <>
                            <TextField
                                margin="normal"
                                required
                                fullWidth
                                name="password"
                                label="Password"
                                type={showPassword ? 'text' : 'password'}
                                id="password"
                                autoComplete="new-password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                InputProps={{
                                    endAdornment: (
                                        <InputAdornment position="end">
                                            <IconButton
                                                aria-label="toggle password visibility"
                                                onClick={handleClickShowPassword}
                                                edge="end"
                                            >
                                                {showPassword ? <VisibilityOff /> : <Visibility />}
                                            </IconButton>
                                        </InputAdornment>
                                    ),
                                }}
                            />
                            <TextField
                                margin="normal"
                                required
                                fullWidth
                                name="confirmPassword"
                                label="Confirm Password"
                                type={showPassword ? 'text' : 'password'}
                                id="confirmPassword"
                                autoComplete="new-password"
                                value={confirmPassword}
                                onChange={(e) => setConfirmPassword(e.target.value)}
                            />
                            <FormControl fullWidth className="mb-3">
                                <InputLabel id="employee-select-label">Employee</InputLabel>
                                <Select
                                    labelId="employee-select-label"
                                    id="employee-select"
                                    value={selectedEmployee}
                                    onChange={(e) => setSelectedEmployee(e.target.value)}
                                    label="Employee"
                                >
                                    {employees.map((employee) => (
                                        <MenuItem key={employee.id} value={employee.id}>
                                            {employee.firstName} {employee.lastName}
                                        </MenuItem>
                                    ))}
                                </Select>
                            </FormControl>
                        </>
                    ) : ''}
                    {errorText && <Typography color="error">{errorText}</Typography>}
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                        onClick={handleSubmit}
                    >
                        {isEditing ? 'Update User' : 'Create User'}
                    </Button>
                </Box>
            </Modal>

            <Modal open={roleOpen} onClose={handleRoleClose}>
                <Box sx={{ ...style, width: 400 }}>
                    <Typography variant="h6" component="h2">
                        Assign Role
                    </Typography>
                    <FormControl fullWidth>
                        <InputLabel>Role</InputLabel>
                        <Select
                            value={selectedRole}
                            onChange={(e) => setSelectedRole(e.target.value)}
                            input={<OutlinedInput label="Role" />}
                        >
                            {roles.map((role) => (
                                <MenuItem key={role.id} value={role.name}>
                                    <ListItemText primary={role.name} />
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    {errorText && <Typography color="error">{errorText}</Typography>}
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                        onClick={handleRoleSubmit}
                    >
                        Assign Role
                    </Button>
                </Box>
            </Modal>
        </Card>
    );
};

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
};

export default UserList;
