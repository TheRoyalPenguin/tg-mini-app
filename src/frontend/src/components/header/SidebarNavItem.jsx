import { useNavigate } from 'react-router-dom';

const SidebarNavItem = ({ to, label, icon, onClick }) => {
    const navigate = useNavigate();

    return (
        <button
            onClick={() => { navigate(to); onClick(); }}
            className="flex items-center space-x-3 text-left text-gray-700 hover:text-black transition text-[17px] border-b"
        >
            <img src={icon} alt={label} className="w-7 h-7" />
            <span>{label}</span>
        </button>
    );
};

export default SidebarNavItem;
