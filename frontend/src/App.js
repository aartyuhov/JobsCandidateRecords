// App.js
import React from 'react';
import Login from './components/Auth/Login';
import {Route, Routes, useNavigate} from 'react-router-dom';
import Layout from './Layout/Layout';
import Dashboard from './components/Other/Dashboard';
import Account from './components/Profile/For user/Account';
import Security from "./components/Profile/For user/Security";
import NotExistPage from "./components/Other/NotExistPage";
import ProtectedRoute from "./components/small-components/ProtectedRoute";
import EmployeesList from "./components/Profile/For HR/EmployeeList";
import EmployeeCard from "./components/Profile/For HR/EmployeeCard";
import PositionsManagement from "./components/Position/PositionsManagment";
import CandidateList from "./components/Candidates/CandidateList";
import EmployeeEdit from "./components/Profile/For HR/EmployeeEdit";
import RequestsManager from "./components/RequestForEmpoyee/RequestsManager";
import CandidateProfile from "./components/Candidates/CandidateProfile";
import ApplicationPage from "./components/Candidates/ApplicationPage";
import DepartmentsManagement from "./components/Department/DepartmentsManagement";
import CompaniesManagement from "./components/Company/CompaniesManagement";
import ForgotPassword from "./components/ForgotPassword/ForgotPassword";
import UserList from "./components/User/UserList";
import ListRoles from "./components/Role/ListRoles";
import RoleProtectedRoute from "./components/small-components/RoleProtectedRoute";

const App = () => {
  const [username, setUsername] = React.useState(localStorage.getItem('username'));
  const [userRoles, setUserRoles] = React.useState(JSON.parse(localStorage.getItem('roles')) || []);
  const navigate = useNavigate();

  const logoutHandler = () => {
    setUsername(null);
    setUserRoles([]);
    localStorage.removeItem('username');
    localStorage.removeItem('roles'); // Обновлено
    localStorage.removeItem('auth_token');
    localStorage.removeItem('refresh_token');
    navigate('/login');
  }


  return (
      <div className='App'>
        <Routes>
          <Route element={<ProtectedRoute isAllowed={!username} redirectPath={"/"}/>}>
            <Route path="login" element={<Login/>}/>
            <Route path="forgot-password" element={<ForgotPassword/>}/>
          </Route>
          <Route element={
            <Layout username={username} userRoles={userRoles} logoutHandler={logoutHandler}>
              <ProtectedRoute isAllowed={!!username}/>
            </Layout>}>
            {/*=======For authorized users=========*/}
            <Route path="" element={<Dashboard/>}/>
            <Route path="account" element={<Account/>}/>
            <Route path="security" element={<Security/>}/>

            <Route path="employees" element={
              <RoleProtectedRoute roles={userRoles} allowedRoles={['HR', 'Admin', 'Head of Department']}>
                <EmployeesList/>
              </RoleProtectedRoute>
            }/>
            <Route path="employeecard" element={
              <RoleProtectedRoute roles={userRoles} allowedRoles={['HR', 'Admin']}>
                <EmployeeCard/>
              </RoleProtectedRoute>
            }/>
            <Route path="/employeeedit/:id" element={
              <RoleProtectedRoute roles={userRoles} allowedRoles={['HR', 'Admin']}>
                <EmployeeEdit/>
              </RoleProtectedRoute>
            }/>

            <Route path="candidates" element={
              <RoleProtectedRoute roles={userRoles} allowedRoles={['HR', 'Admin', 'Head of Department']}>
                <CandidateList/>
              </RoleProtectedRoute>
            }/>
            <Route path="/candidates/new" element={
              <RoleProtectedRoute roles={userRoles} allowedRoles={['HR', 'Admin', 'Head of Department']}>
                <CandidateProfile/>
              </RoleProtectedRoute>
            }/>
            <Route path="/candidates/:id" element={
              <RoleProtectedRoute roles={userRoles} allowedRoles={['HR', 'Admin', 'Head of Department']}>
                <CandidateProfile/>
              </RoleProtectedRoute>
            }/>
            <Route path="/application/:id" element={
              <RoleProtectedRoute roles={userRoles} allowedRoles={['HR', 'Admin', 'Head of Department']}>
                <ApplicationPage/>
              </RoleProtectedRoute>
            }/>

            <Route path="myrequests" element={
              <RoleProtectedRoute roles={userRoles} allowedRoles={['HR', 'Admin', 'Head of Department']}>
                <RequestsManager viewType="my"/>
              </RoleProtectedRoute>
            }/>
            <Route path="allrequests" element={
              <RoleProtectedRoute roles={userRoles} allowedRoles={['HR', 'Admin', 'Head of Department']}>
                <RequestsManager viewType="all"/>
              </RoleProtectedRoute>
            }/>

            {/*=======!For authorized users=========*/}

            {/*=======For admin=========*/}
            <Route path="positions" element={
              <RoleProtectedRoute roles={userRoles} allowedRoles={['Admin']}>
                <PositionsManagement/>
              </RoleProtectedRoute>
            }/>
            <Route path="departments" element={
              <RoleProtectedRoute roles={userRoles} allowedRoles={['Admin']}>
                <DepartmentsManagement/>
              </RoleProtectedRoute>
            }/>
            <Route path="companies" element={
              <RoleProtectedRoute roles={userRoles} allowedRoles={['Admin']}>
                <CompaniesManagement/>
              </RoleProtectedRoute>
            }/>
            <Route path="roles" element={
              <RoleProtectedRoute roles={userRoles} allowedRoles={['Admin']}>
                <ListRoles/>
              </RoleProtectedRoute>
            }/>
            <Route path="users" element={
              <RoleProtectedRoute roles={userRoles} allowedRoles={['Admin']}>
                <UserList/>
              </RoleProtectedRoute>
            }/>
            {/*=======!For admin=========*/}
          </Route>
          <Route path="*" element={<NotExistPage/>}/>
        </Routes>
      </div>
  );
};

export default App;
