import './index.css'
import { NavLink, Outlet } from '../node_modules/react-router/dist/development/index'
import { Enums } from "./Enums.js"

function Header()
{
    return (
        <>
            <header>
                <nav>
                    <ul>
                        <li>
                            <NavLink to="/">Home</NavLink>
                        </li>
                        <li>
                            <NavLink to="/about">About</NavLink>
                        </li>
                        {sessionStorage.getItem(Enums.currentUser) &&
                            <li>
                                <NavLink to="/info">Info</NavLink>
                            </li>
                        }
                        <li>
                            <NavLink to="/login">Login</NavLink>
                        </li>
                    </ul>
                </nav>
            </header>
            <Outlet/>
        </>
    )
}

export default Header