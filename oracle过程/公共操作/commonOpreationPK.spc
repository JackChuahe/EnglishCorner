create or replace package commonOpreationPK is
       /*
           ���������İ�,��������ҳ�涼����з��ʵİ�������
       */
       function getHeadImgAndUserEmail(v_userEmail in varchar2,v_lastname out varchar2) return varchar2;          -- �����û�����,���û������򷵻��û�ͷ���url ����Ϊnull
end;
/
