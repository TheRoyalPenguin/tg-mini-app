function LongreadHeader({ title, description }) {
    return (
        <>
            <h1 className="text-4xl font-bold mb-4 text-center text-gray-800">{title}</h1>
            <p className="text-lg text-gray-600 mb-8 text-center">{description}</p>
        </>
    );
}

export default LongreadHeader;
