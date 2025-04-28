import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Menu, X } from 'lucide-react';
import Sidebar from './Sidebar';

const Header = ({ showBackButton = true, backgroundColor = 'bg-[#f7f8fc]' }) => {
    const [isSidebarOpen, setIsSidebarOpen] = useState(false);
    const navigate = useNavigate();

    const toggleSidebar = () => setIsSidebarOpen(!isSidebarOpen);

    return (
        <>
            <div className={`fixed top-0 left-0 right-0 flex justify-between items-center px-4 py-2 z-40 pointer-events-none ${backgroundColor}`}>
                {showBackButton && (
                    <button
                        onClick={() => navigate(-1)}
                        className="pointer-events-auto text-gray-800"
                    >
                        <svg xmlns="http://www.w3.org/2000/svg" className="w-7 h-7" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 19l-7-7 7-7" />
                        </svg>
                    </button>
                )}
                <button
                    onClick={toggleSidebar}
                    className="pointer-events-auto text-gray-800"
                >
                    {isSidebarOpen ? <X size={28} /> : <Menu size={28} />}
                </button>
            </div>

            {/* Можно добавить заглушку если надо отступ для страниц */}
            {/* <div className="h-14" /> */}

            <Sidebar isOpen={isSidebarOpen} toggleSidebar={toggleSidebar} />
        </>
    );
};

export default Header;
