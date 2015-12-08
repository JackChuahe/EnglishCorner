create or replace package body commonOpreationPK is

       -- function 
       function getHeadImgAndUserEmail(v_userEmail in varchar2,v_lastname out varchar2 ) return varchar2 is
          -- 传入用户邮箱,若用户存在则返回用户头像的url 否则为null
          userHeadImg varchar2(50);
       begin
           select HEADIMAGEURL,(firstname || ' ' || lastname)  into userHeadImg,v_lastname  from allusers where userEmail = v_userEmail;      -- 找出该变量
           return userHeadImg;
       exception
           when no_data_found then
             return null;
       end getHeadImgAndUserEmail;

end;
/
