import React from 'react';
import { Navigate } from 'react-router-dom';

const RoleProtectedRoute = ({ roles, allowedRoles, children }) => {
    const hasPermission = allowedRoles.some(role => roles.includes(role));
    return hasPermission ? children : <Navigate to="/" />;
};

export default RoleProtectedRoute;
