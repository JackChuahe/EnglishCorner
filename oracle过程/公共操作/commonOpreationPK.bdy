create or replace package body commonOpreationPK is

       -- function 
       function getHeadImgAndUserEmail(v_userEmail in varchar2,v_lastname out varchar2 ) return varchar2 is
          -- �����û�����,���û������򷵻��û�ͷ���url ����Ϊnull
          userHeadImg varchar2(50);
       begin
           select HEADIMAGEURL,(firstname || ' ' || lastname)  into userHeadImg,v_lastname  from allusers where userEmail = v_userEmail;      -- �ҳ��ñ���
           return userHeadImg;
       exception
           when no_data_found then
             return null;
       end getHeadImgAndUserEmail;

end;
/
