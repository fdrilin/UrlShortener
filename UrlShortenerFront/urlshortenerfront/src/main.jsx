import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import { BrowserRouter, Routes, Route } from '../node_modules/react-router/dist/development/index'
import Header from './Header.jsx'
import Login from './Login.jsx'
import Info from './Info.jsx'
import { Enums } from './Enums.js'

createRoot(document.getElementById('root')).render(
    <StrictMode>
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Header />} >
                    <Route index element={<App />} />
                    <Route path="/login" element={<Login />} />
                    { sessionStorage.getItem(Enums.currentUser) &&
                        <Route path="/info" element={<Info />} />
                    }
                </Route>
            </Routes>
        </BrowserRouter>
  </StrictMode>,
)
