import React, { useEffect } from 'react';
import { Navigate } from 'react-router-dom';
import { useAuth } from '../hooks/use-auth';

const RequireAdmin = ({ children }) => {
  const user = useAuth();
  debugger
  if (!user.isAdmin) {
    return <Navigate to='/' />;
  }


  return children;
};

export default RequireAdmin;
