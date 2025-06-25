import { useState } from "react";
import { NavLink as RRNavLink } from "react-router-dom";
import {
  Button,
  Collapse,
  Nav,
  NavLink,
  NavItem,
  Navbar,
  NavbarBrand,
  NavbarToggler,
} from "reactstrap";
import { logout } from "../managers/authManager";

export default function NavBar({ loggedInUser, setLoggedInUser }) {
  const [open, setOpen] = useState(false);

  const toggleNavbar = () => setOpen(!open);

  return (
    <div>
      <Navbar color="light" light expand="lg" className="mb-4 shadow-sm">
        <div className="container">
          <NavbarBrand tag={RRNavLink} to="/" className="fw-bold">
            Origami Helper
          </NavbarBrand>
          <NavbarToggler onClick={toggleNavbar} />
          <Collapse isOpen={open} navbar>
            <Nav className="me-auto" navbar>
              <NavItem>
                <NavLink tag={RRNavLink} to="/models">
                  Models
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={RRNavLink} to="/models/create">
                  Create Model
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={RRNavLink} to="/requests">
                  Help Requests
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={RRNavLink} to={`/users/${loggedInUser?.id}`}>
                  Profile
                </NavLink>
              </NavItem>
            </Nav>
            {loggedInUser ? (
              <Button
                color="outline-danger"
                onClick={(e) => {
                  e.preventDefault();
                  logout().then(() => setLoggedInUser(null));
                }}
              >
                Logout
              </Button>
            ) : (
              <Nav navbar>
                <NavItem>
                  <NavLink tag={RRNavLink} to="/login">
                    <Button color="primary">Login</Button>
                  </NavLink>
                </NavItem>
              </Nav>
            )}
          </Collapse>
        </div>
      </Navbar>
    </div>
  );
}
