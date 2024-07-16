// Layout.js
import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Layout.css';
import { Avatar } from '@mui/material';

const Layout = ({ children }) => {
  const [isSidebarVisible, setSidebarVisible] = useState(false);

  const toggleSidebar = () => {
    setSidebarVisible(!isSidebarVisible);
  };

  return (
    <div className="d-flex min-vh-100">
      {isSidebarVisible && (
        <aside className="bg-secondary text-white min-vh-100" style={{ width: '250px' }}>
          <nav>
            <div className="d-flex justify-content-between align-items-center p-4">
              <div className="text-lg fw-bold">
                <CustomerServiceIcon /><span className='px-3'>HR app</span>
              </div>
              <i className="nav-item btn-sm btn-light cursor-pointer" onClick={toggleSidebar}>
                <SidebarRightIcon />
              </i>
            </div>
            <ul className="list-unstyled">
              <li className="mb-1 d-flex align-items-center cursor-pointer">
                <Link to="/dashboard" className="nav-item text-white no-underline">
                  <DashboardIcon /> <span className='px-3'>Dashboard</span>
                </Link>
              </li>
              <li className="mb-1 d-flex align-items-center cursor-pointer">
                <Link to="/shortcuts" className="nav-item text-white no-underline">
                  <ShortcutsIcon /> <span className='px-3'>Shortcuts</span>
                </Link>
              </li>
              {/* Add more navigation items as needed */}
            </ul>
          </nav>
        </aside>
      )}
      <div className="flex-grow-1 p-4 d-flex flex-column">
        <div className="d-flex justify-content-between align-items-center border-bottom pb-3 mb-3">
          <div className="d-flex align-items-center">
            {!isSidebarVisible && (
              <i className="nav-item btn-sm btn-light me-3 cursor-pointer" onClick={toggleSidebar}>
                <SidebarLeftIcon />
              </i>
            )}
          </div>
          <div className="d-flex align-items-center ms-auto">
            <Link to="/account" className="nav-item fw-bold no-underline cursor-pointer">
                  <span className="me-3">Test User</span>
            </Link>
            <Avatar alt="User Avatar" src="https://get.wallhere.com/photo/face-field-Person-man-skarf-adventurer-johannes-strate-776717.jpg" />
            <i className="nav-item cursor-pointer" onClick={() => console.log('Logout')}>
              <LogoutIcon />
            </i>
          </div>
        </div>
        <div className="flex-grow-1">
          {children}
        </div>
      </div>
    </div>
  );
};

const DashboardIcon = () => (
  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width={24} height={24} color={"#000000"} fill={"none"}>
    <path d="M2 6C2 4.11438 2 3.17157 2.58579 2.58579C3.17157 2 4.11438 2 6 2C7.88562 2 8.82843 2 9.41421 2.58579C10 3.17157 10 4.11438 10 6V8C10 9.88562 10 10.8284 9.41421 11.4142C8.82843 12 7.88562 12 6 12C4.11438 12 3.17157 12 2.58579 11.4142C2 10.8284 2 9.88562 2 8V6Z" stroke="currentColor" strokeWidth="1.5" />
    <path d="M2 19C2 18.0681 2 17.6022 2.15224 17.2346C2.35523 16.7446 2.74458 16.3552 3.23463 16.1522C3.60218 16 4.06812 16 5 16H7C7.93188 16 8.39782 16 8.76537 16.1522C9.25542 16.3552 9.64477 16.7446 9.84776 17.2346C10 17.6022 10 18.0681 10 19C10 19.9319 10 20.3978 9.84776 20.7654C9.64477 21.2554 9.25542 21.6448 8.76537 21.8478C8.39782 22 7.93188 22 7 22H5C4.06812 22 3.60218 22 3.23463 21.8478C2.74458 21.6448 2.35523 21.2554 2.15224 20.7654C2 20.3978 2 19.9319 2 19Z" stroke="currentColor" strokeWidth="1.5" />
    <path d="M14 16C14 14.1144 14 13.1716 14.5858 12.5858C15.1716 12 16.1144 12 18 12C19.8856 12 20.8284 12 21.4142 12.5858C22 13.1716 22 14.1144 22 16V18C22 19.8856 22 20.8284 21.4142 21.4142C20.8284 22 19.8856 22 18 22C16.1144 22 15.1716 22 14.5858 21.4142C14 20.8284 14 19.8856 14 18V16Z" stroke="currentColor" strokeWidth="1.5" />
    <path d="M14 5C14 4.06812 14 3.60218 14.1522 3.23463C14.3552 2.74458 14.7446 2.35523 15.2346 2.15224C15.6022 2 16.0681 2 17 2H19C19.9319 2 20.3978 2 20.7654 2.15224C21.2554 2.35523 21.6448 2.74458 21.8478 3.23463C22 3.60218 22 4.06812 22 5C22 5.93188 22 6.39782 21.8478 6.76537C21.6448 7.25542 21.2554 7.64477 20.7654 7.84776C20.3978 8 19.9319 8 19 8H17C16.0681 8 15.6022 8 15.2346 7.84776C14.7446 7.64477 14.3552 7.25542 14.1522 6.76537C14 6.39782 14 5.93188 14 5Z" stroke="currentColor" strokeWidth="1.5" />
  </svg>
);

const ShortcutsIcon = () => (
  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width={24} height={24} color={"#000000"} fill={"none"}>
    <path d="M15 9V5M15 5V1M15 5H11M15 5H19" stroke="currentColor" strokeWidth="1.5" />
    <path d="M12 5C12 4.06812 12 3.60218 12.1522 3.23463C12.3552 2.74458 12.7446 2.35523 13.2346 2.15224C13.6022 2 14.0681 2 15 2C15.9319 2 16.3978 2 16.7654 2.15224C17.2554 2.35523 17.6448 2.74458 17.8478 3.23463C18 3.60218 18 4.06812 18 5C18 5.93188 18 6.39782 17.8478 6.76537C17.6448 7.25542 17.2554 7.64477 16.7654 7.84776C16.3978 8 15.9319 8 15 8C14.0681 8 13.6022 8 13.2346 7.84776C12.7446 7.64477 12.3552 7.25542 12.1522 6.76537C12 6.39782 12 5.93188 12 5Z" stroke="currentColor" strokeWidth="1.5" />
    <path d="M15 15V11M15 15V19M15 15H19M15 15H11" stroke="currentColor" strokeWidth="1.5" />
    <path d="M12 15C12 14.0681 12 13.6022 12.1522 13.2346C12.3552 12.7446 12.7446 12.3552 13.2346 12.1522C13.6022 12 14.0681 12 15 12C15.9319 12 16.3978 12 16.7654 12.1522C17.2554 12.3552 17.6448 12.7446 17.8478 13.2346C18 13.6022 18 14.0681 18 15C18 15.9319 18 16.3978 17.8478 16.7654C17.6448 17.2554 17.2554 17.6448 16.7654 17.8478C16.3978 18 15.9319 18 15 18C14.0681 18 13.6022 18 13.2346 17.8478C12.7446 17.6448 12.3552 17.2554 12.1522 16.7654C12 16.3978 12 15.9319 12 15Z" stroke="currentColor" strokeWidth="1.5" />
  </svg>
);

const SidebarRightIcon = () => (
  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width={24} height={24} color={"#000000"} fill={"none"}>
    <path d="M13 17L17 12L13 7" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
    <path d="M17 12H7" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
  </svg>
);

const SidebarLeftIcon = () => (
  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width={24} height={24} color={"#000000"} fill={"none"}>
    <path d="M11 7L7 12L11 17" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
    <path d="M7 12H17" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
  </svg>
);

const CustomerServiceIcon = () => (
  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width={24} height={24} color={"#000000"} fill={"none"}>
    <path d="M20 17.2603V17C20 14.7909 18.2091 13 16 13H8C5.79086 13 4 14.7909 4 17V17.2603C2.83481 18.1767 2 19.5097 2 21C2 22.6569 3.34315 24 5 24H19C20.6569 24 22 22.6569 22 21C22 19.5097 21.1652 18.1767 20 17.2603Z" stroke="currentColor" strokeWidth="1.5" />
    <path d="M15 9C15 7.67392 14.4732 6.40215 13.5355 5.46447C12.5979 4.52678 11.3261 4 10 4C8.67392 4 7.40215 4.52678 6.46447 5.46447C5.52678 6.40215 5 7.67392 5 9" stroke="currentColor" strokeWidth="1.5" />
    <path d="M17.4 7C17.7314 6.67471 18.1102 6.41347 18.5205 6.23223C18.9307 6.051 19.3659 5.95229 19.8075 5.9413C20.2491 5.93031 20.6884 6.00721 21.101 6.16755C21.5136 6.32789 21.891 6.56896 22.2142 6.87603C22.5374 7.1831 22.8007 7.5494 22.9896 7.95491C23.1785 8.36041 23.2888 8.79606 23.3137 9.24008C23.3386 9.68411 23.2776 10.1286 23.1345 10.5475C22.9915 10.9664 22.7705 11.3507 22.4853 11.6818C22.2001 12.013 21.8571 12.2842 21.475 12.48" stroke="currentColor" strokeWidth="1.5" />
  </svg>
);

const LogoutIcon = () => (
  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width={24} height={24} color="#000000" fill="none">
    <path d="M6 9V15M10 12H3M16 16L21 12L16 8M3 21H14C15.1046 21 16 20.1046 16 19V5C16 3.89543 15.1046 3 14 3H3" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
  </svg>
);

export default Layout;
