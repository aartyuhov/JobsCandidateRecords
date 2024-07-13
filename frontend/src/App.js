// App.js
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Layout from './components/Layout/Layout';
import Dashboard from './components/Sidebar/Dashboard';
import Shortcuts from './components/Sidebar/Shortcuts';
import ProfileSettings from './components/Profile/ProfileSettings';

const App = () => {
  return (
    <div className='App'>
      <Router>
        <Layout>
          <Routes>
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/shortcuts" element={<Shortcuts />} />
            <Route path="/profilesettings" element={<ProfileSettings />} />
            {/* Add more routes as needed */}
          </Routes>
        </Layout>
      </Router>
    </div>
  );
};

export default App;
