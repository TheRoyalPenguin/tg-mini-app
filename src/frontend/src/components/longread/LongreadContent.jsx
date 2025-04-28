import ShadowDOM from 'react-shadow';

function LongreadContent({ styleContent, htmlContent }) {
    return (
        <ShadowDOM.div>
            <style>{styleContent}</style>
            <div dangerouslySetInnerHTML={{ __html: htmlContent }} />
        </ShadowDOM.div>
    );
}

export default LongreadContent;
