import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

const cardClasses = 'bg-light rounded shadow-lg p-4';
const buttonClasses = 'btn btn-primary';

const EmployeeCard = ({ avatarSrc, employeeName }) => (
  <li className="d-flex align-items-center justify-content-between py-3 border-bottom">
    <div className="d-flex align-items-center">
      <img src={avatarSrc} alt="employee-avatar" className="rounded-circle me-3" style={{ width: '40px', height: '40px' }} />
      <span className="fw-medium fs-5 text-primary">{employeeName}</span>
    </div>
    <button className={buttonClasses}>View Profile</button>
  </li>
);

const LeaveRequestCard = ({ title, content }) => (
  <div className="bg-secondary text-white p-4 rounded shadow">
    <h3 className="fs-4 fw-semibold mb-3">{title}</h3>
    <p className="text-muted">{content}</p>
  </div>
);

const Dashboard = () => {
  return (
    <div className="bg-white text-dark min-vh-100 p-4">
      <h1 className="display-4 fw-bold mb-6">HR Dashboard</h1>

      <div className={cardClasses}>
        <h2 className="fs-3 fw-semibold mb-4">Employee List</h2>
        <ul className="list-unstyled">
          <EmployeeCard avatarSrc="https://placehold.co/40x40?text=👩" employeeName="Jane Doe" />
          <EmployeeCard avatarSrc="https://placehold.co/40x40?text=👨" employeeName="John Smith" />
        </ul>
      </div>

      <div className={`${cardClasses} mt-4`}>
        <h2 className="fs-3 fw-semibold mb-4">Leave Requests</h2>
        <div className="row row-cols-1 row-cols-md-2 g-4">
          <div className="col">
            <LeaveRequestCard title="Vacation Leave" content="No pending requests" />
          </div>
          <div className="col">
            <LeaveRequestCard title="Sick Leave" content="1 pending request" />
          </div>
        </div>
      </div>

      <div className={`${cardClasses} mt-4`}>
        <h2 className="fs-3 fw-semibold mb-4">Performance Overview</h2>
        <div className="row row-cols-1 row-cols-md-2 g-4">
          <div className="col">
            <LeaveRequestCard title="Top Performers" content="John Smith, Jane Doe" />
          </div>
          <div className="col">
            <LeaveRequestCard title="Improvement Areas" content="Communication Skills" />
          </div>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
