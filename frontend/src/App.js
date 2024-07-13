// App.js
import React from 'react';
import LoginForm from './components/LoginForm';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Layout from './components/Layout/Layout';
import Dashboard from './components/Sidebar/Dashboard';
import Shortcuts from './components/Sidebar/Shortcuts';
import ProfileSettings from './components/Profile/ProfileSettings';
import axios from 'axios';

const App = () => {
  axios.defaults.baseURL = 'https://localhost:7087/';
  //axios.defaults.xsrfHeaderName = "X-CSRFTOKEN";
  //axios.defaults.xsrfCookieName = "csrftoken";
  return (
    <div className='App'>
      <Router>
        <Layout>
          <Routes>
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/shortcuts" element={<Shortcuts />} />
            <Route path="/profilesettings" element={<ProfileSettings />} />
          </Routes>
        </Layout>
      </Router>
      {/* <LoginForm></LoginForm> */}
    </div>
  );
};

export default App;
