import { X } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import SidebarNavItem from './SidebarNavItem';
import {parseJwt} from "../../services/parseJwt";

const Sidebar = ({ isOpen, toggleSidebar }) => {
    const navigate = useNavigate();

    const authToken = localStorage.getItem('authToken');
    const payload =  authToken ? parseJwt(authToken) : null;
    const userId = payload?.nameid;

    const menuItems = [
        { path: userId ? `/profile/${userId}` : '/profile', label: 'Профиль', icon: '/images/profile.svg' },
        { path: '/courses', label: 'Курсы', icon: '/images/course.svg' },
        { path: '/faq', label: 'FAQ', icon: '/images/faq.svg' },
        { path: '/support', label: 'Поддержка', icon: '/images/support.svg' },
    ];

    return (
        <>
            <div
                className={`fixed top-0 right-0 h-full w-56 bg-white shadow-lg transform transition-transform duration-300 z-50 flex flex-col ${
                    isOpen ? 'translate-x-0' : 'translate-x-full'
                }`}
            >
                <div className="flex items-center justify-between px-4 py-3 bg-gray-100">
                    <img src="/images/logo.png" alt="логотип" className="h-8 w-auto" />
                    <button onClick={toggleSidebar} className="text-gray-800">
                        <X size={28} />
                    </button>
                </div>

                <nav className="flex flex-col px-4 space-y-5 mt-5">
                    {menuItems.map((item) => (
                        <SidebarNavItem
                            key={item.path}
                            to={item.path}
                            label={item.label}
                            icon={item.icon}
                            onClick={toggleSidebar}
                        />
                    ))}
                </nav>

                <div className="mt-auto px-4 py-6 text-gray-500 text-sm">
                    <p>© БАРС Груп, 2025</p>
                </div>
            </div>

            {isOpen && (
                <div className="fixed inset-0 bg-black/40 z-40" onClick={toggleSidebar}></div>
            )}
        </>
    );
};

export default Sidebar;
