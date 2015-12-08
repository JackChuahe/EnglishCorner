create or replace package UserVerifyPK is
/*
用户验证的包，实现用户验证和用户注册的全部基本功能
*/
function getUserHeadImg(email in varchar2 )return  varchar2;        -- 获取用户头像url 通过传输来 email 信息 返回用户的头像url
function  verifyLogin(v_userEmail in varchar2 ,v_pwd in varchar2) return varchar2;        -- 验证用户名和密码是否正确      
  function isAlreadyExistsAccount(v_userEmail in varchar2) return varchar2;          --  判断用户输入的邮箱是否存在 ,存在返回 '1' ，不存在返回 '0'   
function insertUserBaseInfo(v_userEmail in varchar2,v_pwd in varchar2,first_name in varchar2,last_name in varchar) return varchar2;       
  -- 插入基本信息,当用户在注册使得时候
  function userActive(v_userEmail in varchar2) return varchar2;   --为用户激活,若用户未激活,则激活并返回1 成功 。否则返回 0 失败
end;
/
