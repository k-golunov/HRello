import React from 'react';


const FormFrame = ({ onSubmit, children }) => {
  return (
    <div className='form_frame' onClick={(e) => e.stopPropagation()}>
      <div className='formframe_head'>
        {/*<NavLogo />*/}
      </div>
      <form className='formframe_body' onSubmit={onSubmit}>
        {children}
      </form>
    </div>
  );
};

export default FormFrame;
