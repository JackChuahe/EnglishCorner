create or replace package UserVerifyPK is
/*
�û���֤�İ���ʵ���û���֤���û�ע���ȫ����������
*/
function getUserHeadImg(email in varchar2 )return  varchar2;        -- ��ȡ�û�ͷ��url ͨ�������� email ��Ϣ �����û���ͷ��url
function  verifyLogin(v_userEmail in varchar2 ,v_pwd in varchar2) return varchar2;        -- ��֤�û����������Ƿ���ȷ      
  function isAlreadyExistsAccount(v_userEmail in varchar2) return varchar2;          --  �ж��û�����������Ƿ���� ,���ڷ��� '1' �������ڷ��� '0'   
function insertUserBaseInfo(v_userEmail in varchar2,v_pwd in varchar2,first_name in varchar2,last_name in varchar) return varchar2;       
  -- ���������Ϣ,���û���ע��ʹ��ʱ��
  function userActive(v_userEmail in varchar2) return varchar2;   --Ϊ�û�����,���û�δ����,�򼤻����1 �ɹ� �����򷵻� 0 ʧ��
end;
/
