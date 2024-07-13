import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

const INPUT_CLASS = "form-control mt-1";
const LABEL_CLASS = "form-label";

const ProfileSettings = () => {
  return (
    <div className="d-flex align-items-center justify-content-center bg-light py-6 px-5">
      <div className="shadow-sm rounded-lg max-w-4xl w-100 d-flex flex-column flex-md-row">
        
        <div className="w-md-25 bg-primary text-white p-4 d-flex flex-column align-items-center">
          <img src="https://placehold.co/100x100" alt="profile-picture" className="rounded-circle mb-4" />
          <h2 className="h5 font-weight-semibold">Edogaru</h2>
          <p className="small">edogaru@mail.com.my</p>
        </div>
        
        <div className="w-100 w-md-75 p-4">
          <h2 className="h4 font-weight-semibold mb-4">Profile Settings</h2>
          <form className="space-y-4">
            <div className="d-flex flex-column flex-md-row space-y-4 space-md-y-0 space-md-x-4">
              <div className="w-100 w-md-50">
                <label className={LABEL_CLASS}>First Name</label>
                <input type="text" className={INPUT_CLASS} placeholder="first name" />
              </div>
              <div className="w-100 w-md-50">
                <label className={LABEL_CLASS}>Surname</label>
                <input type="text" className={INPUT_CLASS} placeholder="surname" />
              </div>
            </div>
            <div>
              <label className={LABEL_CLASS}>Phone Number</label>
              <input type="text" className={INPUT_CLASS} placeholder="enter phone number" />
            </div>
            <div>
              <label className={LABEL_CLASS}>Address Line 1</label>
              <input type="text" className={INPUT_CLASS} placeholder="enter address line 1" />
            </div>
            <div>
              <label className={LABEL_CLASS}>Address Line 2</label>
              <input type="text" className={INPUT_CLASS} placeholder="enter address line 2" />
            </div>
            <div>
              <label className={LABEL_CLASS}>Email</label>
              <input type="email" className={INPUT_CLASS} placeholder="enter email id" />
            </div>
            <div>
              <label className={LABEL_CLASS}>Education</label>
              <input type="text" className={INPUT_CLASS} placeholder="education" />
            </div>
            <div className="d-flex flex-column flex-md-row space-y-4 space-md-y-0 space-md-x-4">
              <div className="w-100 w-md-50">
                <label className={LABEL_CLASS}>Country</label>
                <input type="text" className={INPUT_CLASS} placeholder="country" />
              </div>
              <div className="w-100 w-md-50">
                <label className={LABEL_CLASS}>State/Region</label>
                <input type="text" className={INPUT_CLASS} placeholder="state" />
              </div>
            </div>
            <button type="submit" className="btn btn-primary mt-4">Save Profile</button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default ProfileSettings;
