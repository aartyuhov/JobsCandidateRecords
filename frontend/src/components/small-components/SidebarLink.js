import React from 'react';
import { Link } from 'react-router-dom';

const SidebarLink = ({ iconSrc, text, href }) => (
    <li className="list-group-item p-2">
      <Link to={href} className="d-flex align-items-center text-decoration-none text-secondary p-2 rounded no-underline">
          <img src={iconSrc} alt={text} className="me-3" style={{ width: '24px', height: '24px' }} />
          {text}
      </Link>
    </li>
  );

export default SidebarLink;