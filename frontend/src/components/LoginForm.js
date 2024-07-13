import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';

const LoginForm = () => {
  const handleSubmit = async (event) => {
    event.preventDefault();
    const username = event.target.username.value;//не использовал
    const password = event.target.password.value;// не использовал
    try {
      await axios.get('api/AcademicSubject').then(resp => console.log(resp));
    }
    catch (e) {
        console.error(e.message);
    }
  };


  return (
    <div className="d-flex min-vh-100 align-items-center justify-content-center bg-white">
      <div className="card shadow-lg p-4 w-100" style={{ maxWidth: '400px', backgroundColor: '#f8f9fa' }}>
        <h2 className="text-center mb-4 text-dark">HR Base Login</h2>
        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label htmlFor="username" className="form-label text-dark" style={{ textAlign: 'left' }}>Username</label>
            <input
              id="username"
              type="text"
              className="form-control"
              placeholder="Enter your username"
              style={{ backgroundColor: '#fff', borderColor: '#ced4da', color: '#495057' }}
            />
          </div>
          <div className="mb-3">
            <label htmlFor="password" className="form-label text-dark" style={{ textAlign: 'left' }}>Password</label>
            <input
              id="password"
              type="password"
              className="form-control"
              placeholder="Enter your password"
              style={{ backgroundColor: '#fff', borderColor: '#ced4da', color: '#495057' }}
            />
          </div>
          <button type="submit" className="btn btn-dark w-100">
            Login
          </button>
        </form>
      </div>
    </div>
  );
};

export default LoginForm;
