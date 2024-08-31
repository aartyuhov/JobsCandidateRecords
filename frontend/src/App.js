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

const App = () => {

  const [username, setUsername] = React.useState(localStorage.getItem('username'));
  const navigate = useNavigate();

  const logoutHandler = () => {
    setUsername(null);
    localStorage.removeItem('username');
    localStorage.removeItem('auth_token');
    localStorage.removeItem('refresh_token');
    navigate('/login');
  }

  return (
    <div className='App'>
        <Routes>
          <Route element={ <ProtectedRoute isAllowed={!username} redirectPath={"/"}/> }>
            <Route path="login" element={<Login />} />
          </Route>
          <Route element={
            <Layout username={username} logoutHandler={logoutHandler}>
              <ProtectedRoute isAllowed={!!username}/>
            </Layout> }>
            {/*=======For authorized users=========*/}
            <Route path="" element={<Dashboard />} />
            <Route path="account" element={<Account />} />
            <Route path="security" element={<Security />} />

            <Route path="employees" element={<EmployeesList />} />
            <Route path="employeecard" element={<EmployeeCard />} />
            <Route path="/employeeedit/:id" element={<EmployeeEdit />} />

            <Route path="candidates" element={<CandidateList />} />
            <Route path="/candidates/new" element={<CandidateProfile />} />
            <Route path="/candidates/:id" element={<CandidateProfile />} />
            <Route path="/application/:id" element={<ApplicationPage />} />

            <Route path="myrequests" element={<RequestsManager viewType="my" />} />
            <Route path="allrequests" element={<RequestsManager viewType="all" />} />

            {/*=======!For authorized users=========*/}
            
            
            {/*=======For admin=========*/}
            <Route path="positions" element={<PositionsManagement />} />
            {/*=======!For admin=========*/}
          </Route>
          <Route path="*" element={<NotExistPage />} />
        </Routes>
    </div>
  );
};

export default App;
